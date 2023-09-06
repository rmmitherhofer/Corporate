using Api.Core;
using Candidatos.Business.Models;
using FluentValidation.Results;
using Logs.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Candidatos.Data;

public class CandidatoContext : DbContext, IUnitOfWork
{
    private const string DataCadastro = nameof(DataCadastro);
    private const string DataAlteracao = nameof(DataAlteracao);

    public CandidatoContext(DbContextOptions<CandidatoContext> options) : base(options) { }

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
