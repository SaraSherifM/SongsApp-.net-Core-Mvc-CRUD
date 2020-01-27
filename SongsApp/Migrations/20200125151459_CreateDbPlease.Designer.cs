﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SongsApp.Models;

namespace SongsApp.Migrations
{
    [DbContext(typeof(SongDb))]
    [Migration("20200125151459_CreateDbPlease")]
    partial class CreateDbPlease
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SongsApp.Models.Singer", b =>
                {
                    b.Property<int>("SingerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("SingerId1")
                        .HasColumnType("int");

                    b.Property<string>("SingerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SingerId");

                    b.HasIndex("SingerId1");

                    b.ToTable("Singers");
                });

            modelBuilder.Entity("SongsApp.Models.Song", b =>
                {
                    b.Property<int>("SongId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AlbumName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReleaseDate")
                        .HasColumnType("int");

                    b.Property<int>("SingerId")
                        .HasColumnType("int");

                    b.Property<string>("SongName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SongId");

                    b.HasIndex("SingerId");

                    b.ToTable("Songs");
                });

            modelBuilder.Entity("SongsApp.Models.Singer", b =>
                {
                    b.HasOne("SongsApp.Models.Singer", null)
                        .WithMany("Singers")
                        .HasForeignKey("SingerId1");
                });

            modelBuilder.Entity("SongsApp.Models.Song", b =>
                {
                    b.HasOne("SongsApp.Models.Singer", "Singers")
                        .WithMany()
                        .HasForeignKey("SingerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
