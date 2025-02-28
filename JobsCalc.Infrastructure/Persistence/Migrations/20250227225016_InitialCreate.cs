using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobsCalc.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "varchar(150)", nullable: false),
                    Email = table.Column<string>(type: "varchar(150)", nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(64)", nullable: false),
                    AvatarUrl = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "jobs",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    DailyHours = table.Column<int>(type: "integer", nullable: false),
                    TotalHours = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobs", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_jobs_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "plannings",
                columns: table => new
                {
                    PlanningId = table.Column<Guid>(type: "uuid", nullable: false),
                    MonthlyBudget = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    DaysPerWeek = table.Column<int>(type: "integer", nullable: false),
                    HoursPerDay = table.Column<int>(type: "integer", nullable: false),
                    VacationPerYear = table.Column<int>(type: "integer", nullable: true),
                    ValueHour = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plannings", x => x.PlanningId);
                    table.ForeignKey(
                        name: "FK_plannings_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_jobs_UserId",
                table: "jobs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_plannings_UserId",
                table: "plannings",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "jobs");

            migrationBuilder.DropTable(
                name: "plannings");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
