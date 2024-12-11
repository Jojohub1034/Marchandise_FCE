using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Marchandise_FCE.Migrations
{
    /// <inheritdoc />
    public partial class initialisation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    codcat = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false),
                    designcat = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.codcat);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Expediteurs",
                columns: table => new
                {
                    idexp = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false),
                    nomexp = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    cin = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false),
                    tel = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expediteurs", x => x.idexp);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    codezone = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    nomzone = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    descriptzone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.codezone);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Marchandises",
                columns: table => new
                {
                    refmarch = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false),
                    nature = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    codcat = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marchandises", x => x.refmarch);
                    table.ForeignKey(
                        name: "FK_Marchandises_Categories_codcat",
                        column: x => x.codcat,
                        principalTable: "Categories",
                        principalColumn: "codcat",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Gares",
                columns: table => new
                {
                    codegare = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    nomgare = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    abrevnom = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    kilometrage = table.Column<int>(type: "int", nullable: false),
                    codezone = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    Mgarecodegare = table.Column<string>(type: "varchar(3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gares", x => x.codegare);
                    table.ForeignKey(
                        name: "FK_Gares_Gares_Mgarecodegare",
                        column: x => x.Mgarecodegare,
                        principalTable: "Gares",
                        principalColumn: "codegare");
                    table.ForeignKey(
                        name: "FK_Gares_Zones_codezone",
                        column: x => x.codezone,
                        principalTable: "Zones",
                        principalColumn: "codezone",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tarifs",
                columns: table => new
                {
                    idtarif = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    prixkilo = table.Column<int>(type: "int", nullable: false),
                    codcat = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: false),
                    codezone = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarifs", x => x.idtarif);
                    table.ForeignKey(
                        name: "FK_Tarifs_Categories_codcat",
                        column: x => x.codcat,
                        principalTable: "Categories",
                        principalColumn: "codcat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tarifs_Zones_codezone",
                        column: x => x.codezone,
                        principalTable: "Zones",
                        principalColumn: "codezone",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Envois",
                columns: table => new
                {
                    numenvoi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    idexp = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false),
                    codegare = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    refmarch = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: false),
                    nbenvoi = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    poids = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    type_embal = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Envois", x => x.numenvoi);
                    table.ForeignKey(
                        name: "FK_Envois_Expediteurs_idexp",
                        column: x => x.idexp,
                        principalTable: "Expediteurs",
                        principalColumn: "idexp",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Envois_Gares_codegare",
                        column: x => x.codegare,
                        principalTable: "Gares",
                        principalColumn: "codegare",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Envois_Marchandises_refmarch",
                        column: x => x.refmarch,
                        principalTable: "Marchandises",
                        principalColumn: "refmarch",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Envois_codegare",
                table: "Envois",
                column: "codegare");

            migrationBuilder.CreateIndex(
                name: "IX_Envois_idexp",
                table: "Envois",
                column: "idexp");

            migrationBuilder.CreateIndex(
                name: "IX_Envois_refmarch",
                table: "Envois",
                column: "refmarch");

            migrationBuilder.CreateIndex(
                name: "IX_Gares_codezone",
                table: "Gares",
                column: "codezone");

            migrationBuilder.CreateIndex(
                name: "IX_Gares_Mgarecodegare",
                table: "Gares",
                column: "Mgarecodegare");

            migrationBuilder.CreateIndex(
                name: "IX_Marchandises_codcat",
                table: "Marchandises",
                column: "codcat");

            migrationBuilder.CreateIndex(
                name: "IX_Tarifs_codcat",
                table: "Tarifs",
                column: "codcat");

            migrationBuilder.CreateIndex(
                name: "IX_Tarifs_codezone",
                table: "Tarifs",
                column: "codezone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Envois");

            migrationBuilder.DropTable(
                name: "Tarifs");

            migrationBuilder.DropTable(
                name: "Expediteurs");

            migrationBuilder.DropTable(
                name: "Gares");

            migrationBuilder.DropTable(
                name: "Marchandises");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
