using Api.Core;
using Clientes.Business.Models;
using FluentValidation.Results;
using Logs.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Data;

public class ClienteContext : DbContext, IUnitOfWork
{
    private const string RegistrationDate = nameof(RegistrationDate);
    private const string DateChange = nameof(DateChange);

    public ClienteContext(DbContextOptions<ClienteContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e =>
                e.GetProperties()
                .Where(p => p.ClrType == typeof(string))))
        {
            property.SetColumnType("varchar(100)");
        }

        modelBuilder.Ignore<ValidationResult>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClienteContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        }
        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty(RegistrationDate) != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(RegistrationDate).CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(RegistrationDate).IsModified = false;

                entry.Property(DateChange).CurrentValue = DateTime.Now;
            }
        }

        try
        {
            return await base.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            ConsoleLog.LogFail(ex.Message);
            throw;
        }
    }
}
