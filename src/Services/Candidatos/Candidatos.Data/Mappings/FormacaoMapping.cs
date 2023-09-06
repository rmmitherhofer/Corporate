using Candidatos.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candidatos.Data.Mappings;

public class FormacaoMapping : IEntityTypeConfiguration<Formacao>
{
    public void Configure(EntityTypeBuilder<Formacao> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.DataCadastro)
            .IsRequired();

        builder.Property(f => f.DataAlteracao);

        builder.Property(f => f.NomeInstituicao)
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder.Property(f => f.Descricao)
            .HasColumnType("varchar(150)");

        builder.Property(f => f.DataInicio);

        builder.Property(f => f.DataFim);

        builder.Property(f => f.Tipo)
            .IsRequired();

        builder.Property(f => f.Ensino);

        builder.Property(f => f.Curso);

        builder.Property(f => f.Status)
            .IsRequired();

        builder.HasOne(f => f.Candidato)
            .WithMany(c => c.Formacoes);

        builder.ToTable(nameof(Formacao));
    }
}
