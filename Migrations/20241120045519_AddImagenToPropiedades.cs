using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiDepaEstudiantil.Migrations
{
    /// <inheritdoc />
    public partial class AddImagenToPropiedades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Propiedades",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Propiedades");
        }
    }
}
