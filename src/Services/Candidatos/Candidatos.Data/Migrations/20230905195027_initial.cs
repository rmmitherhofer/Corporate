using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidatos.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "SQ_Candidato");

            migrationBuilder.CreateTable(
                name: "Candidato",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Codigo = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR SQ_Candidato"),
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Apelido = table.Column<string>(type: "varchar(35)", nullable: true),
                    Telefone = table.Column<string>(type: "varchar(15)", nullable: true),
                    Celular = table.Column<string>(type: "varchar(15)", nullable: true),
                    CelularAlternativo = table.Column<string>(type: "varchar(15)", nullable: true),
                    Email = table.Column<string>(type: "varchar(254)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Idade = table.Column<int>(type: "int", nullable: false),
                    Naturalidade = table.Column<string>(type: "varchar(150)", nullable: true),
                    StatusCivil = table.Column<int>(type: "int", nullable: true),
                    PretensaoSalarial = table.Column<string>(type: "varchar(150)", nullable: true),
                    QuantidadeFilhos = table.Column<int>(type: "int", nullable: false),
                    IdadeFilhos = table.Column<string>(type: "varchar(30)", nullable: true),
                    Idiomas = table.Column<string>(type: "varchar(150)", nullable: true),
                    Conhecimentos = table.Column<string>(type: "varchar(1024)", nullable: true),
                    RecrutadoPor = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Tags = table.Column<string>(type: "varchar(2450)", nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(150)", nullable: true),
                    Numero = table.Column<string>(type: "varchar(10)", nullable: true),
                    Complemento = table.Column<string>(type: "varchar(60)", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(120)", nullable: true),
                    Cep = table.Column<string>(type: "varchar(10)", nullable: true),
                    Cidade = table.Column<string>(type: "varchar(120)", nullable: true),
                    Estado = table.Column<string>(type: "varchar(2)", nullable: true),
                    MoraCom = table.Column<string>(type: "varchar(150)", nullable: true),
                    Domicilio = table.Column<int>(type: "int", nullable: true),
                    TipoDomicilio = table.Column<int>(type: "int", nullable: true),
                    Sexo = table.Column<int>(type: "int", nullable: false),
                    RelatoCandidato = table.Column<string>(type: "varchar(1024)", nullable: true),
                    RelatoEntrevistador = table.Column<string>(type: "varchar(1024)", nullable: true),
                    Comentarios = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experiencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Empresa = table.Column<string>(type: "varchar(150)", nullable: false),
                    Cargo = table.Column<string>(type: "varchar(150)", nullable: false),
                    Atividade = table.Column<string>(type: "varchar(2048)", nullable: true),
                    RegimeContratacao = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    MotivoDesligamento = table.Column<string>(type: "varchar(300)", nullable: true),
                    Observacoes = table.Column<string>(type: "varchar(2048)", nullable: true),
                    Habilidade = table.Column<string>(type: "varchar(150)", nullable: true),
                    UltimoSalario = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CandidatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiencia_Candidato_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "Candidato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Formacao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeInstituicao = table.Column<string>(type: "varchar(150)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(150)", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Ensino = table.Column<int>(type: "int", nullable: true),
                    Curso = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CandidatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Formacao_Candidato_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "Candidato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Historico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CandidatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comentario = table.Column<string>(type: "varchar(450)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Historico_Candidato_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "Candidato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Experiencia_CandidatoId",
                table: "Experiencia",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Formacao_CandidatoId",
                table: "Formacao",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Historico_CandidatoId",
                table: "Historico",
                column: "CandidatoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Experiencia");

            migrationBuilder.DropTable(
                name: "Formacao");

            migrationBuilder.DropTable(
                name: "Historico");

            migrationBuilder.DropTable(
                name: "Candidato");

            migrationBuilder.DropSequence(
                name: "SQ_Candidato");
        }
    }
}
