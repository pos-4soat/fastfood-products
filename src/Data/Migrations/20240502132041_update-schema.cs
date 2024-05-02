using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fastfood_products.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateschema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "FastFood");

            migrationBuilder.RenameTable(
                name: "Product",
                newName: "Product",
                newSchema: "FastFood");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Product",
                schema: "FastFood",
                newName: "Product");
        }
    }
}
