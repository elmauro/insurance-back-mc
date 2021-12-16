using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MC.Insurance.Infrastructure.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerInsurances",
                columns: table => new
                {
                    customerInsuranceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    document = table.Column<string>(nullable: true),
                    customerName = table.Column<string>(nullable: true),
                    insuranceId = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    type = table.Column<int>(nullable: false),
                    coverage = table.Column<string>(nullable: true),
                    start = table.Column<DateTime>(nullable: false),
                    period = table.Column<int>(nullable: false),
                    price = table.Column<float>(nullable: false),
                    risk = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInsurances", x => x.customerInsuranceId);
                });

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    insuranceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    type = table.Column<int>(nullable: false),
                    coverage = table.Column<string>(nullable: true),
                    start = table.Column<DateTime>(nullable: false),
                    period = table.Column<int>(nullable: false),
                    price = table.Column<float>(nullable: false),
                    risk = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurances", x => x.insuranceId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerInsurances");

            migrationBuilder.DropTable(
                name: "Insurances");
        }
    }
}
