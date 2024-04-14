using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb1EF.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reasons",
                columns: table => new
                {
                    ReasonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReasonTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reasons", x => x.ReasonId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ApplicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationDateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationDateEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmittDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReasonId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_Applications_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Reasons_ReasonId",
                        column: x => x.ReasonId,
                        principalTable: "Reasons",
                        principalColumn: "ReasonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_EmployeeId",
                table: "Applications",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ReasonId",
                table: "Applications",
                column: "ReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Reasons");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
