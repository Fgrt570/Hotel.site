using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class _555 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookingg_AspNetUsers_UserId",
                table: "Bookingg");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookingg",
                table: "Bookingg");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Bookingg");

            migrationBuilder.RenameTable(
                name: "Bookingg",
                newName: "Bookinggg");

            migrationBuilder.RenameColumn(
                name: "RoomType",
                table: "Bookinggg",
                newName: "PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Bookingg_UserId",
                table: "Bookinggg",
                newName: "IX_Bookinggg_UserId");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Rooms",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "RoomType",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Bookinggg",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Bookinggg",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Bookinggg",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookinggg",
                table: "Bookinggg",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookinggg_RoomId",
                table: "Bookinggg",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookinggg_AspNetUsers_UserId",
                table: "Bookinggg",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookinggg_Rooms_RoomId",
                table: "Bookinggg",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookinggg_AspNetUsers_UserId",
                table: "Bookinggg");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookinggg_Rooms_RoomId",
                table: "Bookinggg");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookinggg",
                table: "Bookinggg");

            migrationBuilder.DropIndex(
                name: "IX_Bookinggg_RoomId",
                table: "Bookinggg");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomType",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Bookinggg");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Bookinggg");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Bookinggg");

            migrationBuilder.RenameTable(
                name: "Bookinggg",
                newName: "Bookingg");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Bookingg",
                newName: "RoomType");

            migrationBuilder.RenameIndex(
                name: "IX_Bookinggg_UserId",
                table: "Bookingg",
                newName: "IX_Bookingg_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Bookingg",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookingg",
                table: "Bookingg",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookingg_AspNetUsers_UserId",
                table: "Bookingg",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
