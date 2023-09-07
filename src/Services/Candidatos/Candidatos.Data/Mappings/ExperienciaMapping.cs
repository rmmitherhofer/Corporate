using Candidatos.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candidatos.Data.Mappings;

public class ExperienciaMapping : IEntityTypeConfiguration<Experiencia>
{
    public void Configure(EntityTypeBuilder<Experiencia> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.DataCadastro)
            .IsRequired();

        builder.Property(e => e.DataAlteracao);

        builder.Property(e => e.Empresa)
            .IsRequired();

        builder.Property(e => e.Cargo)
            .IsRequired();

        builder.Property(e => e.Atividade)
            .HasColumnType("varchar(2048)");

        builder.Property(e => e.Habilidade)
            .HasColumnType("varchar(300)");

        builder.Property(e => e.RegimeContratacao)
            .IsRequired();

        builder.Property(e => e.DataInicio)
            .IsRequired();

        builder.Property(e => e.DataFim);

        builder.Property(e => e.Status)
            .IsRequired();

        builder.Property(e => e.MotivoDesligamento)
            .HasColumnType("varchar(300)");

        builder.Property(e => e.Observacoes)
            .HasColumnType("varchar(2048)");

        builder.Property(e => e.UltimoSalario);

        builder.HasOne(e => e.Candidato)
            .WithMany(c => c.Experiencias);



        builder.ToTable(nameof(Experiencia));
    }
}
