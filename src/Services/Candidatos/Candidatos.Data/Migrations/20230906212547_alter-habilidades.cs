using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Candidatos.Data.Migrations
{
    /// <inheritdoc />
    public partial class alterhabilidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Habilidade",
                table: "Experiencia",
                type: "varchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Habilidade",
                table: "Experiencia",
                type: "varchar(150)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(300)",
                oldNullable: true);
        }
    }
}
