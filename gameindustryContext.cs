using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Sem2Lab1SQLServer;

namespace Sem2Lab1SQLServer
{
    public partial class gameindustryContext : DbContext
    {
        public gameindustryContext()
        {
        }

        public gameindustryContext(DbContextOptions<gameindustryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Countries> Countries { get; set; }
        public virtual DbSet<Critics> Critics { get; set; }
        public virtual DbSet<Developers> Developers { get; set; }
        public virtual DbSet<Games> Games { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Publications> Publications { get; set; }
        public virtual DbSet<Publishers> Publishers { get; set; }
        public virtual DbSet<Ratings> Ratings { get; set; }
        public virtual DbSet<Continents> Continents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-9L7KP7D\\SQLEXPRESS; Database=gamesindustry; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Countries>(entity =>
            {
                entity.HasKey(e => e.CountryId);

                entity.ToTable("countries");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.ContinentId).HasColumnName("continent_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("nvarchar(MAX)");

                entity.HasOne(d => d.Continent)
                    .WithMany(p => p.Countries)
                    .IsRequired()
                    .HasForeignKey(d => d.ContinentId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_continents_countries");
            });

            modelBuilder.Entity<Continents>(entity =>
            {
                entity.HasKey(e => e.ContinentId);

                entity.ToTable("continents");

                entity.Property(e => e.ContinentId).HasColumnName("continent_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Area)
                    .IsRequired()
                    .HasColumnName("area")
                    .HasColumnType("int");

            });

            modelBuilder.Entity<Critics>(entity =>
            {
                entity.HasKey(e => e.CriticId);

                entity.ToTable("critics");

                entity.Property(e => e.CriticId).HasColumnName("critic_id");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("nvarchar(MAX)");
            });

            modelBuilder.Entity<Developers>(entity =>
            {
                entity.HasKey(e => e.DeveloperId);

                entity.ToTable("developers");

                entity.Property(e => e.DeveloperId).HasColumnName("developer_id");

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.FoundationDate)
                    .IsRequired()
                    .HasColumnName("foundation_date")
                    .HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.WorkersNumber).HasColumnName("workers_number");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Developers)
                    .IsRequired()
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_developers_countries");
            }) ;

            modelBuilder.Entity<Games>(entity =>
            {
                entity.HasKey(e => e.GameId);

                entity.ToTable("games");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.Budget)
                    .HasColumnName("budget")
                    .HasColumnType("bigint");

                entity.Property(e => e.DeveloperId).HasColumnName("developer_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("nvarchar(MAX)");

                entity.HasOne(d => d.Developer)
                    .WithMany(p => p.Games)
                    .IsRequired()
                    .HasForeignKey(d => d.DeveloperId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_games_developers");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Games)
                    .IsRequired()
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_games_genres");
            });

            modelBuilder.Entity<Genres>(entity =>
            {
                entity.HasKey(e => e.GenreId);

                entity.ToTable("genres");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("nvarchar(MAX)");
            });

            modelBuilder.Entity<Publications>(entity =>
            {
                entity.HasKey(e => e.PublicationId);

                entity.ToTable("publications");

                entity.Property(e => e.PublicationId).HasColumnName("publication_id");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.PublisherId).HasColumnName("publisher_id");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Publications)
                    .IsRequired()
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_publications_games");

                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Publications)
                    .IsRequired()
                    .HasForeignKey(d => d.PublisherId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_publications_publishers");
            });

            modelBuilder.Entity<Publishers>(entity =>
            {
                entity.HasKey(e => e.PublisherId);

                entity.ToTable("publishers");

                entity.Property(e => e.PublisherId).HasColumnName("publisher_id");

                entity.Property(e => e.Contacts)
                    .IsRequired()
                    .HasColumnName("contacts")
                    .HasColumnType("nvarchar(MAX)");

                entity.Property(e => e.Earnings)
                    .HasColumnName("earnings")
                    .HasColumnType("bigint");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("nvarchar(MAX)");
            });

            modelBuilder.Entity<Ratings>(entity =>
            {
                entity.HasKey(e => e.RatingId);

                entity.ToTable("ratings");

                entity.Property(e => e.RatingId).HasColumnName("rating_id");

                entity.Property(e => e.CriticId).HasColumnName("critic_id");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.Mark).HasColumnName("mark");

                entity.HasOne(d => d.Critic)
                    .WithMany(p => p.Ratings)
                    .IsRequired()
                    .HasForeignKey(d => d.CriticId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ratings_critics");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Ratings)
                    .IsRequired()
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ratings_games");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
