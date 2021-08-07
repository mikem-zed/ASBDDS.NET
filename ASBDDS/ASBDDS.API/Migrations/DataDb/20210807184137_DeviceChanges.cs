using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASBDDS.API.Migrations.DataDb
{
    public partial class DeviceChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Projects_ProjectId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_ProjectId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "BaseModel",
                table: "Devices",
                newName: "Manufacturer");

            migrationBuilder.RenameColumn(
                name: "RentEnd",
                table: "DeviceRents",
                newName: "Created");

            migrationBuilder.AddColumn<DateTime>(
                name: "Closed",
                table: "DeviceRents",
                type: "timestamp without time zone",
                nullable: true);

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
                name: "Closed",
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

            migrationBuilder.RenameColumn(
                name: "Manufacturer",
                table: "Devices",
                newName: "BaseModel");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "DeviceRents",
                newName: "RentEnd");

            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                table: "Devices",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "Devices",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ProjectId",
                table: "Devices",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Projects_ProjectId",
                table: "Devices",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
