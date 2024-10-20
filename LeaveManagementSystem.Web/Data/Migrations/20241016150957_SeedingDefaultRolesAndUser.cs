using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultRolesAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeName",
                table: "LeaveTypes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LeaveTypes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1cf5cd75-5863-47dc-9a97-c5e5245e60b8", null, "Supervisor", "SUPERVISOR" },
                    { "3c07dac8-1279-4790-ab57-da007eeb91cb", null, "Employee", "EMPLOYEE" },
                    { "67520f68-f21b-40b5-a298-43e115b819b6", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "4d3fa1bc-ffa1-4e69-982b-3dd4978452fe", 0, "8185bd72-cfdc-417a-9a23-e0b27a523a27", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEO19UdO1HR5DrA+0V2g7io2LAmyOm35nfBEEZlGjunfZGpoxv9XjSU8nLfyVL3WeSw==", null, false, "f57e4be5-85fc-40ef-a9fc-1399456f003d", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "67520f68-f21b-40b5-a298-43e115b819b6", "4d3fa1bc-ffa1-4e69-982b-3dd4978452fe" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1cf5cd75-5863-47dc-9a97-c5e5245e60b8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c07dac8-1279-4790-ab57-da007eeb91cb");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "67520f68-f21b-40b5-a298-43e115b819b6", "4d3fa1bc-ffa1-4e69-982b-3dd4978452fe" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67520f68-f21b-40b5-a298-43e115b819b6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d3fa1bc-ffa1-4e69-982b-3dd4978452fe");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "LeaveTypes");

            migrationBuilder.AddColumn<string>(
                name: "TypeName",
                table: "LeaveTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
