﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI.DataContext;

#nullable disable

namespace WebAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240817121901_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebAPI.Entities.BPKB", b =>
                {
                    b.Property<string>("agreement_number")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("bpkb_date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("bpkb_date_in")
                        .HasColumnType("datetime2");

                    b.Property<string>("bpkb_no")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("branch_id")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("created_by")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("created_on")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("faktur_date")
                        .HasColumnType("datetime2");

                    b.Property<string>("faktur_no")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("last_updated_by")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("last_updated_on")
                        .HasColumnType("datetime2");

                    b.Property<string>("location_id1")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("police_no")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("agreement_number");

                    b.HasIndex("location_id1");

                    b.ToTable("tr_bpkb", "dbo");
                });

            modelBuilder.Entity("WebAPI.Entities.StorageLocation", b =>
                {
                    b.Property<string>("location_id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("location_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("location_id");

                    b.ToTable("ms_storage_location", "dbo");
                });

            modelBuilder.Entity("WebAPI.Entities.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("user_id"), 1L, 1);

                    b.Property<bool>("is_active")
                        .HasColumnType("bit");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("user_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("user_id");

                    b.ToTable("ms_user", "dbo");
                });

            modelBuilder.Entity("WebAPI.Entities.BPKB", b =>
                {
                    b.HasOne("WebAPI.Entities.StorageLocation", "location_id")
                        .WithMany()
                        .HasForeignKey("location_id1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("location_id");
                });
#pragma warning restore 612, 618
        }
    }
}
