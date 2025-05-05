// Data/Migrations/[timestamp]_SeedMenuData.cs
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMgmtWeb.Data
{
    public partial class SeedMenuData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Title", "IconClass", "Url", "ParentId", "Order", "IsActive", "RoleRequired" },
                values: new object[,]
                {
                    { 1, "Dashboard", "bi bi-speedometer", "/", null, 1, true, null },
                    { 2, "Employees", "bi bi-people-fill", "/Employees", null, 2, true, null },
                    { 3, "Departments", "bi bi-building", "/Departments", null, 3, true, null },
                    { 4, "Leave Requests", "bi bi-calendar-check", "/LeaveRequests", null, 4, true, null },
                    { 5, "Reports", "bi bi-file-earmark-bar-graph", "#", null, 5, true, "Admin" },
                    { 6, "Employee Report", "bi bi-file-text", "/Reports/Employee", 5, 1, true, "Admin" },
                    { 7, "Leave Report", "bi bi-file-text", "/Reports/Leave", 5, 2, true, "Admin" },
                    { 8, "Administration", "bi bi-gear-fill", "#", null, 6, true, "Admin" },
                    { 9, "Users", "bi bi-person-badge", "/Admin/Users", 8, 1, true, "Admin" },
                    { 10, "Roles", "bi bi-shield-lock", "/Admin/Roles", 8, 2, true, "Admin" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Menus WHERE Id BETWEEN 1 AND 10");
        }
    }
}