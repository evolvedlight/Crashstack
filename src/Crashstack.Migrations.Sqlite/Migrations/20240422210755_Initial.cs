using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crashstack.Migrations.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    IssueSearchField = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Traces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TraceId = table.Column<string>(type: "TEXT", nullable: true),
                    Release = table.Column<string>(type: "TEXT", nullable: true),
                    Environment = table.Column<string>(type: "TEXT", nullable: true),
                    Message = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CrashstackEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EventTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IssueId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Module = table.Column<string>(type: "TEXT", nullable: true),
                    ThreadId = table.Column<string>(type: "TEXT", nullable: true),
                    StackTrace = table.Column<string>(type: "TEXT", nullable: true),
                    Level = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrashstackEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrashstackEvents_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrashstackEvents_IssueId",
                table: "CrashstackEvents",
                column: "IssueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrashstackEvents");

            migrationBuilder.DropTable(
                name: "Traces");

            migrationBuilder.DropTable(
                name: "Issues");
        }
    }
}
