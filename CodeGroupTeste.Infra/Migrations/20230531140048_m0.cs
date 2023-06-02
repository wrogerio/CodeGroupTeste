using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeGroupTeste.Infra.Migrations
{
    /// <inheritdoc />
    public partial class m0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jogos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DtPartida = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    Local = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, defaultValue: ""),
                    Placar = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false, defaultValue: ""),
                    QtdPorTime = table.Column<int>(type: "int", nullable: false, defaultValue: 10),
                    Observacao = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false, defaultValue: ""),
                    IsRealizado = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jogadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false, defaultValue: ""),
                    Nivel = table.Column<int>(type: "int", nullable: false, defaultValue: 3),
                    IsGoleiro = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsConfirmado = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Observacao = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false, defaultValue: ""),
                    JogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jogadores_Jogos_JogoId",
                        column: x => x.JogoId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jogadores_JogoId",
                table: "Jogadores",
                column: "JogoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jogadores");

            migrationBuilder.DropTable(
                name: "Jogos");
        }
    }
}
