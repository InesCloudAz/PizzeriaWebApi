using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedFromBonustPointsToBonusPoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BonustPoints",
                table: "AspNetUsers",
                newName: "BonusPoints");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BonusPoints",
                table: "AspNetUsers",
                newName: "BonustPoints");
        }
    }
}
