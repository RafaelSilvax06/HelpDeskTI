using System;
using HelpDeskTI.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskTI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20260610000100_RemoveComentarios")]
    public partial class RemoveComentarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChamadoId = table.Column<long>(type: "INTEGER", nullable: true),
                    autorId = table.Column<int>(type: "INTEGER", nullable: true),
                    mensagem = table.Column<string>(type: "TEXT", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentarios_Chamados_ChamadoId",
                        column: x => x.ChamadoId,
                        principalTable: "Chamados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comentarios_Usuarios_autorId",
                        column: x => x.autorId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_autorId",
                table: "Comentarios",
                column: "autorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_ChamadoId",
                table: "Comentarios",
                column: "ChamadoId");
        }
    }
}
