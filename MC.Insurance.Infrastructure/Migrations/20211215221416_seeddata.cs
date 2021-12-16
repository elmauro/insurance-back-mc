using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MC.Insurance.Infrastructure.Migrations
{
    public partial class seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "customerId", "customerName", "document" },
                values: new object[] { 1, "Mauricio Cadavid", "98632674" });

            migrationBuilder.InsertData(
                table: "CustomerInsurances",
                columns: new[] { "customerInsuranceId", "coverage", "customerName", "description", "document", "insuranceId", "name", "period", "price", "risk", "start", "type" },
                values: new object[] { 1, "50%", "Mauricio Cadavid", "Seguro de Incendios", "98632674", 1, "Incendios A1", 12, 200000f, 4, new DateTime(2000, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.InsertData(
                table: "Insurances",
                columns: new[] { "insuranceId", "coverage", "description", "name", "period", "price", "risk", "start", "type" },
                values: new object[] { 1, "50%", "Seguro de Incendios", "Incendios A1", 12, 200000f, 4, new DateTime(2000, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customer",
                keyColumn: "customerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CustomerInsurances",
                keyColumn: "customerInsuranceId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Insurances",
                keyColumn: "insuranceId",
                keyValue: 1);
        }
    }
}
