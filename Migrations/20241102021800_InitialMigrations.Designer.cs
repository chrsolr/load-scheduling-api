﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LoadSchedulingAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20241102021800_InitialMigrations")]
    partial class InitialMigrations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "uuid-ossp");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Config", b =>
                {
                    b.Property<Guid>("ConfigId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("config_id")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("brand");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasColumnName("created_by")
                        .HasDefaultValueSql("'iw_db_default_user@innowatts.com'::text");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("is_active");

                    b.Property<string>("Market")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("market");

                    b.Property<string>("Org")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("org");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("UpdatedBy")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasColumnName("updated_by")
                        .HasDefaultValueSql("'iw_db_default_user@innowatts.com'::text");

                    b.HasKey("ConfigId")
                        .HasName("configs_pkey");

                    b.HasIndex(new[] { "Org", "Brand", "Market" }, "configs_org_brand_market_key")
                        .IsUnique();

                    b.ToTable("configs", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}