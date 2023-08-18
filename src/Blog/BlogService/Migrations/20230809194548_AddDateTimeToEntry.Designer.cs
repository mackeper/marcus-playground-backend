﻿// <auto-generated />
using System;
using BlogService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlogService.Migrations
{
    [DbContext(typeof(BlogDbContext))]
    [Migration("20230809194548_AddDateTimeToEntry")]
    partial class AddDateTimeToEntry
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("BlogService.Domain.Entry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Context")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Entries");
                });
#pragma warning restore 612, 618
        }
    }
}
