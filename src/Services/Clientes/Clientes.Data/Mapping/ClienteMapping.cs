using Clientes.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clientes.Data.Mapping;

public class ClienteMapping : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.DataCadastro)
            .IsRequired();

        builder.Property(c => c.DataAlteracao);

        builder.Property(c => c.Cnpj)
            .IsRequired()
            .HasColumnType("varchar(14)");

        builder.Property(c => c.NomeFantasia)
            .IsRequired()
            .HasColumnType("varchar(150)");

        builder.Property(c => c.NomeRepresentante)
            .IsRequired();

        builder.Property(c => c.Telefone)
            .IsRequired();

        builder.Property(c => c.Email)
            .IsRequired();


        builder.ToTable(nameof(Cliente));
    }
}