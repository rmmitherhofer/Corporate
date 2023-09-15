using Clientes.Business.Models;
using Core.ValueObjects;
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
            .HasColumnType("varchar(14)")
            .IsRequired();

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasColumnType("varchar(150)");

        builder.Property(c => c.Representante)
            .HasColumnType("varchar(150)");

        builder.Property(c => c.Telefone)
            .HasColumnType("varchar(15)");

        builder.Property(c => c.Celular)
            .HasColumnType("varchar(15)");

        builder.Property(c => c.Email)
            .HasColumnType($"varchar({Email.AddressMaxLength})");

        builder.OwnsOne(c => c.Endereco, e =>
        {
            e.Property(e => e.Logradouro)
                .HasColumnName(nameof(Endereco.Logradouro))
                .HasColumnType("varchar(150)");

            e.Property(e => e.Numero)
                .HasColumnName(nameof(Endereco.Numero))
                .HasColumnType("varchar(10)");

            e.Property(e => e.Complemento)
                .HasColumnName(nameof(Endereco.Complemento))
                .HasColumnType("varchar(60)");

            e.Property(e => e.Bairro)
                .HasColumnName(nameof(Endereco.Bairro))
                .HasColumnType("varchar(120)");

            e.Property(e => e.Cep)
                .HasColumnName(nameof(Endereco.Cep))
                .HasColumnType("varchar(10)");

            e.Property(e => e.Cidade)
                .HasColumnName(nameof(Endereco.Cidade))
                .HasColumnType("varchar(120)");

            e.Property(e => e.Estado)
                .HasColumnName(nameof(Endereco.Estado))
                .HasColumnType("varchar(2)");
        });

        builder.ToTable(nameof(Cliente));
    }
}