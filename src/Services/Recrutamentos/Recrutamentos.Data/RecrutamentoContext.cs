using Api.Core;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Recrutamentos.Data;

public class RecrutamentoContext : DbContext, IUnitOfWork
{
    private const string RegistrationDate = nameof(RegistrationDate);
    private const string DateChange = nameof(DateChange);

    public RecrutamentoContext(DbContextOptions<RecrutamentoContext> options) : base(options) { }

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

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RecrutamentoContext).Assembly);

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

        return await base.SaveChangesAsync() > 0;
    }
}
