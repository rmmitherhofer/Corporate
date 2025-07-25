using Candidatos.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Zypher.Domain.Core.Enums;
using Zypher.Logs.Extensions;
using Zypher.Notifications.Interfaces;
using Zypher.Notifications.Messages;
using Zypher.Persistence.Abstractions.Data;

namespace Candidatos.Data;

public class CandidatoContext : DbContext, IUnitOfWork
{
    private readonly ILogger<CandidatoContext> _logger;
    private const string RegistrationDate = nameof(RegistrationDate);
    private const string DateChanged = nameof(DateChanged);

    private readonly INotificationHandler _notification;

    public CandidatoContext(DbContextOptions<CandidatoContext> options, INotificationHandler notification, ILogger<CandidatoContext> logger) : base(options)
    {
        _notification = notification;
        _logger = logger;
    }

    public DbSet<Candidato> Candidatos { get; set; }
    public DbSet<Formacao> Formacoes { get; set; }
    public DbSet<Experiencia> Experiencias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e =>
                e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
        {
            property.SetColumnType("varchar(150)");
        }

        modelBuilder.Ignore<ValidationResult>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CandidatoContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }
        modelBuilder.HasSequence<int>("SQ_Candidato").StartsAt(1).IncrementsBy(1);

        base.OnModelCreating(modelBuilder);
    }

    public async Task<(bool, OperationType)> Commit()
    {
        var operationType = ValidateChange();

        var success = await SaveChangesAsync();

        return (success, operationType);
    }

    private async Task<bool> SaveChangesAsync()
    {
        var success = false;
        try
        {
            if (_notification.HasNotifications()) return success;

            success = await base.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            _notification.Notify(new Notification(LogLevel.Critical, ex.GetType().Name, "SQL-Server", ex.Message));

            _logger.LogCrit(ex.Message);
        }
        return success;
    }
    private OperationType ValidateChange()
    {
        OperationType operationType = OperationType.None;
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty(RegistrationDate) != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(RegistrationDate).CurrentValue = DateTime.Now;
                operationType = OperationType.Added;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(RegistrationDate).IsModified = false;

                entry.Property(DateChanged).CurrentValue = DateTime.Now;

                operationType = OperationType.Modified;
            }

            if (entry.State == EntityState.Deleted)
                operationType = OperationType.Deleted;
        }
        return operationType;
    }
}

public class CandidatoContextFactory : IDesignTimeDbContextFactory<CandidatoContext>
{
    public CandidatoContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<CandidatoContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("CNN_DB_CANDIDATO"));

        var logger = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddConsole();
        }).CreateLogger<CandidatoContext>();

        var notification = new FakeNotificationHandler();

        return new CandidatoContext(optionsBuilder.Options, notification, logger);
    }

    // Handler fake para uso apenas em tempo de design
    private class FakeNotificationHandler : INotificationHandler
    {
        public bool HasNotifications() => false;

        public void Notify(Notification notification) { }

        public IEnumerable<Notification> GetNotifications() => Enumerable.Empty<Notification>();

        public void Notify(IEnumerable<Notification> notifications)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Notification> Get()
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}