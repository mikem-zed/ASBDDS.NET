using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASBDDS.API.Migrations.DataDb
{
    public partial class updRents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeviceId",
                table: "DeviceRents",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IPXEUrl",
                table: "DeviceRents",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "DeviceRents",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceRents_DeviceId",
                table: "DeviceRents",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceRents_Devices_DeviceId",
                table: "DeviceRents",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceRents_Devices_DeviceId",
                table: "DeviceRents");

            migrationBuilder.DropIndex(
                name: "IX_DeviceRents_DeviceId",
                table: "DeviceRents");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "DeviceRents");

            migrationBuilder.DropColumn(
                name: "IPXEUrl",
                table: "DeviceRents");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "DeviceRents");
        }
    }
}
