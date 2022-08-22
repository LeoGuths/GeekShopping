using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShopping.ProductAPI.Migrations
{
    public partial class SeedProductDataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "id",
                keyValue: 2L,
                column: "name",
                value: "T-shirt");

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "id",
                keyValue: 3L,
                columns: new[] { "category_name", "name", "price" },
                values: new object[] { "Action Figure", "Bleach Aizen Action Figure PVC", 109.9m });

            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "id", "category_name", "description", "image_url", "name", "price" },
                values: new object[] { 4L, "Sweatshirt", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris ut egestas ipsum, non venenatis nulla. Morbi iaculis aliquam urna. Curabitur at volutpat arcu. Morbi at dictum massa, in sodales velit. Nulla facilisi. Curabitur quam turpis. ", "https://gitlab.com/uploads/-/system/user/avatar/12037378/avatar.png?width=400", "Sweatshirt", 329.9m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "product",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "id",
                keyValue: 2L,
                column: "name",
                value: "Name");

            migrationBuilder.UpdateData(
                table: "product",
                keyColumn: "id",
                keyValue: 3L,
                columns: new[] { "category_name", "name", "price" },
                values: new object[] { "Shirt", "Name", 59.9m });
        }
    }
}
