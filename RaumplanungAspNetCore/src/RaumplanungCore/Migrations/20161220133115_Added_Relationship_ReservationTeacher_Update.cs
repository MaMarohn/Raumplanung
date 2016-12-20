using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RaumplanungCore.Migrations
{
    public partial class Added_Relationship_ReservationTeacher_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Reservations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TeacherId",
                table: "Reservations",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Teachers_TeacherId",
                table: "Reservations",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "TeacherId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Teachers_TeacherId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_TeacherId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Reservations");
        }
    }
}
