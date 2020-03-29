using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sem2Lab1SQLServer.Migrations
{
    public partial class newbase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "continents",
                columns: table => new
                {
                    continent_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    area = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_continents", x => x.continent_id);
                });

            migrationBuilder.CreateTable(
                name: "critics",
                columns: table => new
                {
                    critic_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_critics", x => x.critic_id);
                });

            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    genre_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres", x => x.genre_id);
                });

            migrationBuilder.CreateTable(
                name: "publishers",
                columns: table => new
                {
                    publisher_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    earnings = table.Column<decimal>(type: "money", nullable: false),
                    contacts = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publishers", x => x.publisher_id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    country_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    continent_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.country_id);
                    table.ForeignKey(
                        name: "FK_continents_countries",
                        column: x => x.continent_id,
                        principalTable: "continents",
                        principalColumn: "continent_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "developers",
                columns: table => new
                {
                    developer_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    foundation_date = table.Column<DateTime>(type: "date", nullable: false),
                    workers_number = table.Column<int>(nullable: false),
                    country_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_developers", x => x.developer_id);
                    table.ForeignKey(
                        name: "FK_developers_countries",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "country_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "games",
                columns: table => new
                {
                    game_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    developer_id = table.Column<int>(nullable: false),
                    genre_id = table.Column<int>(nullable: false),
                    budget = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_games", x => x.game_id);
                    table.ForeignKey(
                        name: "FK_games_developers",
                        column: x => x.developer_id,
                        principalTable: "developers",
                        principalColumn: "developer_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_games_genres",
                        column: x => x.genre_id,
                        principalTable: "genres",
                        principalColumn: "genre_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publications",
                columns: table => new
                {
                    publication_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    game_id = table.Column<int>(nullable: false),
                    publisher_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publications", x => x.publication_id);
                    table.ForeignKey(
                        name: "FK_publications_games",
                        column: x => x.game_id,
                        principalTable: "games",
                        principalColumn: "game_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_publications_publishers",
                        column: x => x.publisher_id,
                        principalTable: "publishers",
                        principalColumn: "publisher_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ratings",
                columns: table => new
                {
                    rating_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    critic_id = table.Column<int>(nullable: false),
                    game_id = table.Column<int>(nullable: false),
                    mark = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ratings", x => x.rating_id);
                    table.ForeignKey(
                        name: "FK_ratings_critics",
                        column: x => x.critic_id,
                        principalTable: "critics",
                        principalColumn: "critic_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ratings_games",
                        column: x => x.game_id,
                        principalTable: "games",
                        principalColumn: "game_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_countries_continent_id",
                table: "countries",
                column: "continent_id");

            migrationBuilder.CreateIndex(
                name: "IX_developers_country_id",
                table: "developers",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_games_developer_id",
                table: "games",
                column: "developer_id");

            migrationBuilder.CreateIndex(
                name: "IX_games_genre_id",
                table: "games",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_publications_game_id",
                table: "publications",
                column: "game_id");

            migrationBuilder.CreateIndex(
                name: "IX_publications_publisher_id",
                table: "publications",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "IX_ratings_critic_id",
                table: "ratings",
                column: "critic_id");

            migrationBuilder.CreateIndex(
                name: "IX_ratings_game_id",
                table: "ratings",
                column: "game_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "publications");

            migrationBuilder.DropTable(
                name: "ratings");

            migrationBuilder.DropTable(
                name: "publishers");

            migrationBuilder.DropTable(
                name: "critics");

            migrationBuilder.DropTable(
                name: "games");

            migrationBuilder.DropTable(
                name: "developers");

            migrationBuilder.DropTable(
                name: "genres");

            migrationBuilder.DropTable(
                name: "countries");

            migrationBuilder.DropTable(
                name: "continents");
        }
    }
}
