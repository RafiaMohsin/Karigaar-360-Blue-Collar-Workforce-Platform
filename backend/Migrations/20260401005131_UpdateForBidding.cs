using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karigaar360.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForBidding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Workers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Workers",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TotalJobsCompleted",
                table: "Workers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedAt",
                table: "Jobs",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkerId",
                table: "Jobs",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkerName",
                table: "Jobs",
                type: "TEXT",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_WorkerId",
                table: "Jobs",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Workers_WorkerId",
                table: "Jobs",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Workers_WorkerId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_WorkerId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "TotalJobsCompleted",
                table: "Workers");

            migrationBuilder.DropColumn(
                name: "CompletedAt",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "WorkerId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "WorkerName",
                table: "Jobs");
        }
    }
}
