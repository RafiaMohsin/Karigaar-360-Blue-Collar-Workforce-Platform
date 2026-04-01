using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Karigaar360.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalEarningsToWorker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalEarnings",
                table: "Workers",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalEarnings",
                table: "Workers");
        }
    }
}
