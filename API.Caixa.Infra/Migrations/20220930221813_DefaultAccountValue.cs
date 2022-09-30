using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Caixa.Infra.Migrations
{
    public partial class DefaultAccountValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "LastUpdate", "Value" },
                values: new object[] { new Guid("d65d7e80-5f71-49f2-a19c-e95e67d84f60"), new DateTime(2022, 9, 30, 22, 18, 12, 903, DateTimeKind.Utc).AddTicks(1749), 0.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("d65d7e80-5f71-49f2-a19c-e95e67d84f60"));
        }
    }
}
