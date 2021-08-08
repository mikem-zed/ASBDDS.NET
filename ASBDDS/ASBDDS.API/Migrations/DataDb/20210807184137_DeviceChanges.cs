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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "Manufacturer",
                table: "Devices",
                newName: "BaseModel");

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
