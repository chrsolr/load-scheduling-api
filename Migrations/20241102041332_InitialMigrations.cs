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

            migrationBuilder.CreateTable(
                name: "credentials",
                columns: table => new
                {
                    credential_id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    config_id = table.Column<Guid>(type: "uuid", nullable: false),
                    org = table.Column<string>(type: "text", nullable: false),
                    market = table.Column<string>(type: "text", nullable: false),
                    brand = table.Column<string>(type: "text", nullable: false),
                    zone = table.Column<string>(type: "text", nullable: false),
                    location = table.Column<string>(type: "text", nullable: false),
                    location_name = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    user_account = table.Column<string>(type: "text", nullable: true),
                    user_password = table.Column<string>(type: "text", nullable: true),
                    user_sub_account = table.Column<string>(type: "text", nullable: false, defaultValueSql: "'DEFAULT'::text"),
                    pfx = table.Column<string>(type: "text", nullable: true),
                    pfx_passphrase = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    is_suma = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    use_certificate = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_by = table.Column<string>(type: "text", nullable: false, defaultValueSql: "'iw_db_default_user@innowatts.com'::text"),
                    updated_by = table.Column<string>(type: "text", nullable: false, defaultValueSql: "'iw_db_default_user@innowatts.com'::text"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("credentials_pkey", x => x.credential_id);
                    table.ForeignKey(
                        name: "credentials_config_id_fkey",
                        column: x => x.config_id,
                        principalTable: "configs",
                        principalColumn: "config_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "configs_org_brand_market_key",
                table: "configs",
                columns: new[] { "org", "brand", "market" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "credentials_org_brand_market_zone_location_user_sub_account_key",
                table: "credentials",
                columns: new[] { "org", "brand", "market", "zone", "location", "user_sub_account" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_credentials_config_id",
                table: "credentials",
                column: "config_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "credentials");

            migrationBuilder.DropTable(
                name: "configs");
        }
    }
}
