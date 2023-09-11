using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidatos.Data.Migrations
{
    /// <inheritdoc />
    public partial class cnh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CNH",
                table: "Candidato",
                type: "varchar(15)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CNH",
                table: "Candidato");
        }
    }
}
