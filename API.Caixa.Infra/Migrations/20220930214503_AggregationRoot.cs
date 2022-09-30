using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Caixa.Infra.Migrations
{
    public partial class AggregationRoot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Entries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId1",
                table: "Entries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Entries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entries_AccountId1",
                table: "Entries",
                column: "AccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Accounts_AccountId1",
                table: "Entries",
                column: "AccountId1",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entries_Accounts_AccountId1",
                table: "Entries");

            migrationBuilder.DropIndex(
                name: "IX_Entries_AccountId1",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "Entries");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Entries");
        }
    }
}
