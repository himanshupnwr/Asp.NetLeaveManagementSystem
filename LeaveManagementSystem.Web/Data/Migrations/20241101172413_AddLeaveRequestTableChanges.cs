using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLeaveRequestTableChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_EmployeeId",
                table: "LeaveRequests");

            migrationBuilder.DropColumn(
                name: "LeaveRequestId",
                table: "LeaveRequests");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "LeaveRequests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d3fa1bc-ffa1-4e69-982b-3dd4978452fe",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c60f7d4-bbb2-4b3a-b0bb-8d24ad6ee4ec", "AQAAAAIAAYagAAAAEGAn9QH9NS0BF3MQDGbJp3cI9vZ3/FDY0CNLGnKnSXZVL7UJYe/9ABaF0IZg6A/4dQ==", "f3a11cee-1e79-4da2-82b4-5dcf003fbc91" });

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_EmployeeId",
                table: "LeaveRequests");

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "LeaveRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LeaveRequestId",
                table: "LeaveRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4d3fa1bc-ffa1-4e69-982b-3dd4978452fe",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "951c654e-38b3-46b9-9847-7a86416e5021", "AQAAAAIAAYagAAAAEBSVX/leme+6pXxJ2PeDnDRdAloQzD0Se0IA7Z6T1KoSgeAfO8x0vAbxuVmWHlQH3A==", "a754f27f-993f-4ae8-a749-b07c27331ed6" });

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveRequests_AspNetUsers_EmployeeId",
                table: "LeaveRequests",
                column: "EmployeeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
