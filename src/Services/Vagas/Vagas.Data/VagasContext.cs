using Api.Core;
using FluentValidation.Results;
using Logs.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Notifications.Interfaces;
using Notifications.Messages;
using Vagas.Business.Models;

namespace Vagas.Data;

public class VagasContext : DbContext, IUnitOfWork
{
    private const string DataCadastro = nameof(DataCadastro);
    private const string DataAlteracao = nameof(DataAlteracao);

    private readonly INotificationHandler _notification;

    public VagasContext(DbContextOptions<VagasContext> options, INotificationHandler notification) : base(options)
    {
        _notification = notification;
    }

    public DbSet<Vaga> Vagas { get; set; }
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

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(VagasContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }
        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty(DataCadastro) != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(DataCadastro).CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(DataCadastro).IsModified = false;

                entry.Property(DataAlteracao).CurrentValue = DateTime.Now;
            }
        }

        var sucesso = false;

        try
        {
            if (_notification.HasNotifications()) return sucesso;

            sucesso = await base.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            _notification.Notify(new Notification(LogLevel.Critical, ex.GetType().Name, "SQL-Server", ex.Message));
            ConsoleLog.LogCrit(ex.Message);
        }
        return sucesso;
    }
}
