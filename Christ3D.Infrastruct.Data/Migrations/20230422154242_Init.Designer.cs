﻿// <auto-generated />
using System;
using Christ3D.Infrastruct.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Christ3D.Infrastruct.Data.Migrations
{
    [DbContext(typeof(StudyContext))]
    [Migration("20230422154242_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Christ3D.Domai.Models.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("Id");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Christ3D.Domai.Models.Student", b =>
                {
                    b.OwnsOne("Christ3D.Domai.Models.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("StudentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("County")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Province")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("StudentId");

                            b1.ToTable("Students");

                            b1.WithOwner()
                                .HasForeignKey("StudentId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
