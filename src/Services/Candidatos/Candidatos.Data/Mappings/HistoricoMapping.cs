using Candidatos.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Candidatos.Data.Mappings;

public class HistoricoMapping : IEntityTypeConfiguration<Historico>
{
    public void Configure(EntityTypeBuilder<Historico> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(c => c.RegistrationDate)
            .HasColumnName("DataCadastro")
            .IsRequired();

        builder.Property(c => c.DateChanged)
            .HasColumnName("DataAlteracao");

        builder.Property(f => f.Comentario)
            .HasColumnType("varchar(450)")
            .IsRequired();

        builder.HasOne(h => h.Candidato)
            .WithMany(c => c.Historico);

        builder.ToTable(nameof(Historico));
    }
}