using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "ms_storage_location",
                schema: "dbo",
                columns: table => new
                {
                    location_id = table.Column<string>(type: "varchar(10)", nullable: false),
                    location_name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ms_storage_location", x => x.location_id);
                });

            migrationBuilder.CreateTable(
                name: "ms_user",
                schema: "dbo",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "varchar(20)", nullable: false),
                    password = table.Column<string>(type: "varchar(50)", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ms_user", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "tr_bpkb",
                schema: "dbo",
                columns: table => new
                {
                    agreement_number = table.Column<string>(type: "varchar(100)", nullable: false),
                    bpkb_no = table.Column<string>(type: "varchar(100)", nullable: false),
                    branch_id = table.Column<string>(type: "varchar(10)", nullable: false),
                    bpkb_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    faktur_no = table.Column<string>(type: "varchar(100)", nullable: false),
                    faktur_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    location_id = table.Column<string>(type: "varchar(10)", nullable: false),
                    police_no = table.Column<string>(type: "varchar(20)", nullable: false),
                    bpkb_date_in = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<string>(type: "varchar(20)", nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime2", nullable: true),
                    last_updated_by = table.Column<string>(type: "varchar(20)", nullable: true),
                    last_updated_on = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tr_bpkb", x => x.agreement_number);
                    table.ForeignKey(
                        name: "FK_tr_bpkb_ms_storage_location_location_id",
                        column: x => x.location_id,
                        principalSchema: "dbo",
                        principalTable: "ms_storage_location",
                        principalColumn: "location_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tr_bpkb_location_id",
                schema: "dbo",
                table: "tr_bpkb",
                column: "location_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ms_user",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tr_bpkb",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ms_storage_location",
                schema: "dbo");
        }
    }
}
