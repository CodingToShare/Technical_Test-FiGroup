﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using webapi.Data;

#nullable disable

namespace webapi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230905003219_IdentityUser")]
    partial class IdentityUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("webapi.Models.Task", b =>
                {
                    b.Property<Guid>("TaskID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TaskDescription")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<Guid>("TaskStatusID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TaskID");

                    b.HasIndex("TaskStatusID");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("webapi.Models.TaskStatus", b =>
                {
                    b.Property<Guid>("TaskStatusID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TaskStatusName")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("TaskStatusID");

                    b.ToTable("TaskStatus");

                    b.HasData(
                        new
                        {
                            TaskStatusID = new Guid("5fc73765-cdfe-4d80-80da-b0ed803d0c60"),
                            Created = new DateTime(2023, 9, 4, 19, 32, 18, 978, DateTimeKind.Utc).AddTicks(3495),
                            CreatedBy = "WebAPITask",
                            Modified = new DateTime(2023, 9, 4, 19, 32, 18, 978, DateTimeKind.Utc).AddTicks(3502),
                            ModifiedBy = "WebAPITask",
                            TaskStatusName = "Completada"
                        },
                        new
                        {
                            TaskStatusID = new Guid("cee4a830-f4d0-48ab-87ef-2cc9e1eedb83"),
                            Created = new DateTime(2023, 9, 4, 19, 32, 18, 978, DateTimeKind.Utc).AddTicks(3504),
                            CreatedBy = "WebAPITask",
                            Modified = new DateTime(2023, 9, 4, 19, 32, 18, 978, DateTimeKind.Utc).AddTicks(3505),
                            ModifiedBy = "WebAPITask",
                            TaskStatusName = "No completada"
                        });
                });

            modelBuilder.Entity("webapi.Models.Task", b =>
                {
                    b.HasOne("webapi.Models.TaskStatus", "TaskStatus")
                        .WithMany()
                        .HasForeignKey("TaskStatusID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaskStatus");
                });
#pragma warning restore 612, 618
        }
    }
}
