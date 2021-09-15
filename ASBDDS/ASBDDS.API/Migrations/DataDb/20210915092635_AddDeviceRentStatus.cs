using Microsoft.EntityFrameworkCore.Migrations;

namespace ASBDDS.API.Migrations.DataDb
{
    public partial class AddDeviceRentStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "DeviceRents",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "DeviceRents");
        }
    }
}
