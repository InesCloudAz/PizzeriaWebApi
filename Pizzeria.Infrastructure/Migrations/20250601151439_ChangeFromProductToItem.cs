using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFromProductToItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientItem_Items_ProductsItemId",
                table: "IngredientItem");

            migrationBuilder.RenameColumn(
                name: "ProductsItemId",
                table: "IngredientItem",
                newName: "ItemsItemId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientItem_ProductsItemId",
                table: "IngredientItem",
                newName: "IX_IngredientItem_ItemsItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientItem_Items_ItemsItemId",
                table: "IngredientItem",
                column: "ItemsItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientItem_Items_ItemsItemId",
                table: "IngredientItem");

            migrationBuilder.RenameColumn(
                name: "ItemsItemId",
                table: "IngredientItem",
                newName: "ProductsItemId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientItem_ItemsItemId",
                table: "IngredientItem",
                newName: "IX_IngredientItem_ProductsItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientItem_Items_ProductsItemId",
                table: "IngredientItem",
                column: "ProductsItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
