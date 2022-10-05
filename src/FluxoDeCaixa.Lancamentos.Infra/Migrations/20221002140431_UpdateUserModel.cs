using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FluxoDeCaixa.Caixa.Infra.Migrations
{
    public partial class UpdateUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("20bd7709-aac9-4c25-98c2-f8740652d6ab"));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "LastUpdate", "Value" },
                values: new object[] { new Guid("ad207d72-a2df-4918-86a0-4a9e1546fd68"), new DateTime(2022, 10, 2, 14, 4, 30, 723, DateTimeKind.Utc).AddTicks(3618), 0.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("ad207d72-a2df-4918-86a0-4a9e1546fd68"));

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "LastUpdate", "Value" },
                values: new object[] { new Guid("20bd7709-aac9-4c25-98c2-f8740652d6ab"), new DateTime(2022, 10, 1, 13, 4, 7, 963, DateTimeKind.Utc).AddTicks(527), 0.0 });
        }
    }
}
