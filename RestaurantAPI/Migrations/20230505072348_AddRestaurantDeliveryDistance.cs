using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Migrations
{
    public partial class AddRestaurantDeliveryDistance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasDelivery",
                table: "Restaurants");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryDistance",
                table: "Restaurants",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDistance",
                table: "Restaurants");

            migrationBuilder.AddColumn<bool>(
                name: "HasDelivery",
                table: "Restaurants",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
