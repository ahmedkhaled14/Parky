using Microsoft.EntityFrameworkCore.Migrations;

namespace ParkyAPI.Migrations
{
    public partial class updateTrails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trails_NationalParks_NationalParkId",
                table: "trails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trails",
                table: "trails");

            migrationBuilder.RenameTable(
                name: "trails",
                newName: "Trails");

            migrationBuilder.RenameIndex(
                name: "IX_trails_NationalParkId",
                table: "Trails",
                newName: "IX_Trails_NationalParkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trails",
                table: "Trails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trails_NationalParks_NationalParkId",
                table: "Trails",
                column: "NationalParkId",
                principalTable: "NationalParks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trails_NationalParks_NationalParkId",
                table: "Trails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trails",
                table: "Trails");

            migrationBuilder.RenameTable(
                name: "Trails",
                newName: "trails");

            migrationBuilder.RenameIndex(
                name: "IX_Trails_NationalParkId",
                table: "trails",
                newName: "IX_trails_NationalParkId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_trails",
                table: "trails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_trails_NationalParks_NationalParkId",
                table: "trails",
                column: "NationalParkId",
                principalTable: "NationalParks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
