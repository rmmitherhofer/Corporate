using Candidatos.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Zypher.Domain.Core.ValueObjects;

namespace Candidatos.Data.Mappings;

public class CandidatoMapping : IEntityTypeConfiguration<Candidato>
{
    public void Configure(EntityTypeBuilder<Candidato> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.RegistrationDate)
            .HasColumnName("DataCadastro")
            .IsRequired();

        builder.Property(c => c.DateChanged)
            .HasColumnName("DataAlteracao");

        builder.Property(c => c.Codigo)
                .HasDefaultValueSql("NEXT VALUE FOR SQ_Candidato");

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(c => c.Apelido)
            .HasColumnType("varchar(35)");

        builder.Property(e => e.Telefone)
            .HasColumnType("varchar(15)");

        builder.Property(e => e.Celular)
            .HasColumnType("varchar(15)");

        builder.Property(e => e.CelularAlternativo)
            .HasColumnType("varchar(15)");

        builder.Property(c => c.Email)
            .HasColumnType($"varchar({Email.AddressMaxLength})");


        builder.Property(c => c.DataNascimento);

        builder.Property(c => c.Idade)
            .IsRequired();

        builder.Property(c => c.Naturalidade)
            .HasColumnType("varchar(150)");

        builder.Property(c => c.StatusCivil);

        builder.Property(c => c.PretensaoSalarial)
            .HasColumnType("varchar(150)");

        builder.Property(c => c.QuantidadeFilhos)
            .IsRequired();

        builder.Property(c => c.IdadeFilhos)
            .HasColumnType("varchar(30)");

        builder.Property(c => c.Idiomas)
            .HasColumnType("varchar(150)");

        builder.Property(c => c.Conhecimentos)
            .HasColumnType("varchar(1024)");

        builder.Property(c => c.RecrutadoPor)
            .IsRequired();

        builder.Property(c => c.RelatoEntrevistador)
            .HasColumnType("varchar(1024)");

        builder.Property(c => c.Comentarios)
            .HasColumnType("varchar(MAX)");

        builder.Property(c => c.Status)
            .IsRequired();

        builder.Property(c => c.Tags)
            .HasConversion(
                t => JsonConvert.SerializeObject(t, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                t => JsonConvert.DeserializeObject<List<string>>(t, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }))
            .HasColumnType("varchar(2450)");

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

            e.Property(e => e.MoraCom)
                .HasColumnName(nameof(Endereco.MoraCom))
                .HasColumnType("varchar(150)");

            e.Property(e => e.Domicilio)
                .HasColumnName(nameof(Endereco.Domicilio));

            e.Property(e => e.TipoDomicilio)
                .HasColumnName(nameof(Endereco.TipoDomicilio));
        });

        builder.Property(c => c.Sexo)
            .IsRequired();

        builder.Property(c => c.RelatoCandidato)
            .HasColumnType("varchar(1024)");

        builder.HasMany(c => c.Experiencias)
            .WithOne(xp => xp.Candidato)
            .HasForeignKey(xp => xp.CandidatoId);

        builder.HasMany(c => c.Formacoes)
            .WithOne(f => f.Candidato)
            .HasForeignKey(f => f.CandidatoId);

        builder.HasMany(c => c.Historico)
            .WithOne(f => f.Candidato)
            .HasForeignKey(f => f.CandidatoId);

        builder.ToTable(nameof(Candidato));
    }
}