using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clientes.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cnpj = table.Column<string>(type: "varchar(14)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(150)", nullable: false),
                    Representante = table.Column<string>(type: "varchar(150)", nullable: true),
                    Telefone = table.Column<string>(type: "varchar(15)", nullable: true),
                    Celular = table.Column<string>(type: "varchar(15)", nullable: true),
                    Email = table.Column<string>(type: "varchar(254)", nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(150)", nullable: true),
                    Numero = table.Column<string>(type: "varchar(10)", nullable: true),
                    Complemento = table.Column<string>(type: "varchar(60)", nullable: true),
                    Bairro = table.Column<string>(type: "varchar(120)", nullable: true),
                    Cep = table.Column<string>(type: "varchar(10)", nullable: true),
                    Cidade = table.Column<string>(type: "varchar(120)", nullable: true),
                    Estado = table.Column<string>(type: "varchar(2)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
