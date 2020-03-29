﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sem2Lab1SQLServer;

namespace Sem2Lab1SQLServer.Migrations
{
    [DbContext(typeof(gameindustryContext))]
    partial class gameindustryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sem2Lab1SQLServer.Continents", b =>
                {
                    b.Property<int>("ContinentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("continent_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Area")
                        .HasColumnName("area")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(MAX)");

                    b.HasKey("ContinentId");

                    b.ToTable("continents");
                });

            modelBuilder.Entity("Sem2Lab1SQLServer.Countries", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("country_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContinentId")
                        .HasColumnName("continent_id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(MAX)");

                    b.HasKey("CountryId");

                    b.HasIndex("ContinentId");

                    b.ToTable("countries");
                });

            modelBuilder.Entity("Sem2Lab1SQLServer.Critics", b =>
                {
                    b.Property<int>("CriticId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("critic_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnName("username")
                        .HasColumnType("nvarchar(MAX)");

                    b.HasKey("CriticId");

                    b.ToTable("critics");
                });

            modelBuilder.Entity("Sem2Lab1SQLServer.Developers", b =>
                {
                    b.Property<int>("DeveloperId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("developer_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId")
                        .HasColumnName("country_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("FoundationDate")
                        .HasColumnName("foundation_date")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<int>("WorkersNumber")
                        .HasColumnName("workers_number")
                        .HasColumnType("int");

                    b.HasKey("DeveloperId");

                    b.HasIndex("CountryId");

                    b.ToTable("developers");
                });

            modelBuilder.Entity("Sem2Lab1SQLServer.Games", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("game_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Budget")
                        .HasColumnName("budget")
                        .HasColumnType("money");

                    b.Property<int>("DeveloperId")
                        .HasColumnName("developer_id")
                        .HasColumnType("int");

                    b.Property<int>("GenreId")
                        .HasColumnName("genre_id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(MAX)");

                    b.HasKey("GameId");

                    b.HasIndex("DeveloperId");

                    b.HasIndex("GenreId");

                    b.ToTable("games");
                });

            modelBuilder.Entity("Sem2Lab1SQLServer.Genres", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("genre_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(MAX)");

                    b.HasKey("GenreId");

                    b.ToTable("genres");
                });

            modelBuilder.Entity("Sem2Lab1SQLServer.Publications", b =>
                {
                    b.Property<int>("PublicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("publication_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GameId")
                        .HasColumnName("game_id")
                        .HasColumnType("int");

                    b.Property<int>("PublisherId")
                        .HasColumnName("publisher_id")
                        .HasColumnType("int");

                    b.HasKey("PublicationId");

                    b.HasIndex("GameId");

                    b.HasIndex("PublisherId");

                    b.ToTable("publications");
                });

            modelBuilder.Entity("Sem2Lab1SQLServer.Publishers", b =>
                {
                    b.Property<int>("PublisherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("publisher_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contacts")
                        .IsRequired()
                        .HasColumnName("contacts")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<decimal>("Earnings")
                        .HasColumnName("earnings")
                        .HasColumnType("money");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .HasColumnType("nvarchar(MAX)");

                    b.HasKey("PublisherId");

                    b.ToTable("publishers");
                });

            modelBuilder.Entity("Sem2Lab1SQLServer.Ratings", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("rating_id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CriticId")
                        .HasColumnName("critic_id")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnName("game_id")
                        .HasColumnType("int");

                    b.Property<int>("Mark")
                        .HasColumnName("mark")
                        .HasColumnType("int");

                    b.HasKey("RatingId");

                    b.HasIndex("CriticId");

                    b.HasIndex("GameId");

                    b.ToTable("ratings");
                });

            modelBuilder.Entity("Sem2Lab1SQLServer.Countries", b =>
                {
                    b.HasOne("Sem2Lab1SQLServer.Continents", "Continent")
                        .WithMany("Countries")
                        .HasForeignKey("ContinentId")
                        .HasConstraintName("FK_continents_countries")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sem2Lab1SQLServer.Developers", b =>
                {
                    b.HasOne("Sem2Lab1SQLServer.Countries", "Country")
                        .WithMany("Developers")
                        .HasForeignKey("CountryId")
                        .HasConstraintName("FK_developers_countries")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sem2Lab1SQLServer.Games", b =>
                {
                    b.HasOne("Sem2Lab1SQLServer.Developers", "Developer")
                        .WithMany("Games")
                        .HasForeignKey("DeveloperId")
                        .HasConstraintName("FK_games_developers")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sem2Lab1SQLServer.Genres", "Genre")
                        .WithMany("Games")
                        .HasForeignKey("GenreId")
                        .HasConstraintName("FK_games_genres")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sem2Lab1SQLServer.Publications", b =>
                {
                    b.HasOne("Sem2Lab1SQLServer.Games", "Game")
                        .WithMany("Publications")
                        .HasForeignKey("GameId")
                        .HasConstraintName("FK_publications_games")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sem2Lab1SQLServer.Publishers", "Publisher")
                        .WithMany("Publications")
                        .HasForeignKey("PublisherId")
                        .HasConstraintName("FK_publications_publishers")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sem2Lab1SQLServer.Ratings", b =>
                {
                    b.HasOne("Sem2Lab1SQLServer.Critics", "Critic")
                        .WithMany("Ratings")
                        .HasForeignKey("CriticId")
                        .HasConstraintName("FK_ratings_critics")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sem2Lab1SQLServer.Games", "Game")
                        .WithMany("Ratings")
                        .HasForeignKey("GameId")
                        .HasConstraintName("FK_ratings_games")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
