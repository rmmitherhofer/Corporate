using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vagas.Business.Models;

namespace Vagas.Data.Mappings;

public class VagaMapping : IEntityTypeConfiguration<Vaga>
{
    public void Configure(EntityTypeBuilder<Vaga> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.DataCadastro)
            .IsRequired();

        builder.Property(c => c.DataAlteracao);

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasColumnType("varchar(150)");

        builder.Property(c => c.Descricao)
            .IsRequired()
            .HasColumnType("varchar(150)");

        builder.Property(c => c.ClienteId)
            .IsRequired();

        builder.ToTable(nameof(Vaga));
    }
}