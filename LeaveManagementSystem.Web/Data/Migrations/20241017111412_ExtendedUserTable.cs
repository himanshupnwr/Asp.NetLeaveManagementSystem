using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d3fa1bc-ffa1-4e69-982b-3dd4978452fe",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c189a012-68a9-46cf-9b13-96dc3f2a82b0", new DateOnly(19, 12, 1), "Default", "Admin", "AQAAAAIAAYagAAAAEP/fE7ms+ALGAp5cnxfmDvXS1qt/YdMD5eOsIQr9s18wGzuj7vS04NFDoHP1MiExGQ==", "d5c93841-effa-475f-95a9-9c4f12eb2d40" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d3fa1bc-ffa1-4e69-982b-3dd4978452fe",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8185bd72-cfdc-417a-9a23-e0b27a523a27", "AQAAAAIAAYagAAAAEO19UdO1HR5DrA+0V2g7io2LAmyOm35nfBEEZlGjunfZGpoxv9XjSU8nLfyVL3WeSw==", "f57e4be5-85fc-40ef-a9fc-1399456f003d" });
        }
    }
}
