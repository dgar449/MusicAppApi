﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicAppApi.Data.Models.Context;

#nullable disable

namespace MusicAppApi.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221223071428_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MusicAppApi.Data.Models.Context.Album", b =>
                {
                    b.Property<int>("AlbumID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AlbumID"), 1L, 1);

                    b.Property<string>("AlbumName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ArtistID")
                        .HasColumnType("int");

                    b.Property<string>("Length")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReleaseDate")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AlbumID");

                    b.HasIndex("ArtistID");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("MusicAppApi.Data.Models.Context.Artist", b =>
                {
                    b.Property<int>("ArtistID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArtistID"), 1L, 1);

                    b.Property<string>("ArtistName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArtistID");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("MusicAppApi.Data.Models.Context.Genre", b =>
                {
                    b.Property<int>("GenreID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreID"), 1L, 1);

                    b.Property<string>("GenreType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreID");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("MusicAppApi.Data.Models.Context.Song", b =>
                {
                    b.Property<int>("SongID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SongID"), 1L, 1);

                    b.Property<int>("AlbumID")
                        .HasColumnType("int");

                    b.Property<int>("ArtistID")
                        .HasColumnType("int");

                    b.Property<int>("GenreID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SongTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrackLength")
                        .HasColumnType("int");

                    b.HasKey("SongID");

                    b.HasIndex("AlbumID");

                    b.HasIndex("ArtistID");

                    b.HasIndex("GenreID");

                    b.ToTable("Song");
                });

            modelBuilder.Entity("MusicAppApi.Data.Models.Context.Album", b =>
                {
                    b.HasOne("MusicAppApi.Data.Models.Context.Artist", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("MusicAppApi.Data.Models.Context.Song", b =>
                {
                    b.HasOne("MusicAppApi.Data.Models.Context.Album", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MusicAppApi.Data.Models.Context.Artist", "Artist")
                        .WithMany("Songs")
                        .HasForeignKey("ArtistID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MusicAppApi.Data.Models.Context.Genre", "Genre")
                        .WithMany("Songs")
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Album");

                    b.Navigation("Artist");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("MusicAppApi.Data.Models.Context.Album", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("MusicAppApi.Data.Models.Context.Artist", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("MusicAppApi.Data.Models.Context.Genre", b =>
                {
                    b.Navigation("Songs");
                });
#pragma warning restore 612, 618
        }
    }
}
