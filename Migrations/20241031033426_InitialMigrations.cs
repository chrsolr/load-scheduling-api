using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoadSchedulingAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "configs",
                columns: table => new
                {
                    config_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    org = table.Column<string>(type: "text", nullable: false),
                    market = table.Column<string>(type: "text", nullable: false),
                    brand = table.Column<string>(type: "text", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: false, defaultValueSql: "'iw_db_default_user@innowatts.com'::text"),
                    updated_by = table.Column<string>(type: "text", nullable: false, defaultValueSql: "'iw_db_default_user@innowatts.com'::text"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("configs_pkey", x => x.config_id);
                });

            migrationBuilder.CreateIndex(
                name: "configs_org_brand_market_key",
                table: "configs",
                columns: new[] { "org", "brand", "market" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "configs");
        }
    }
}
