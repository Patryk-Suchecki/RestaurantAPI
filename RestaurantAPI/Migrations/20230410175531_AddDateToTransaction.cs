using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Migrations
{
    public partial class AddDateToTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Transactions_TransactionId",
                table: "Dishes");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AdressId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Dishes_TransactionId",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Dishes");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AdressId",
                table: "Transactions",
                column: "AdressId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transactions_AdressId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Dishes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AdressId",
                table: "Transactions",
                column: "AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_TransactionId",
                table: "Dishes",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Transactions_TransactionId",
                table: "Dishes",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }
    }
}
