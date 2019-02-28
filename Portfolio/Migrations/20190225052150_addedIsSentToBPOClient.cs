using Microsoft.EntityFrameworkCore.Migrations;

namespace BPOSolution.Migrations
{
    public partial class addedIsSentToBPOClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsSent",
                table: "BPOClient",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSent",
                table: "BPOClient");
        }
    }
}
