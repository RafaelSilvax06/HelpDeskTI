using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDeskTI.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoMaisCampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Usuarios",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Usuarios",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "Usuarios",
                newName: "perfil");

            migrationBuilder.AddColumn<string>(
                name: "senha",
                table: "Usuarios",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chamados",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    titulo = table.Column<string>(type: "TEXT", nullable: false),
                    descricao = table.Column<string>(type: "TEXT", nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: false),
                    prioridade = table.Column<int>(type: "INTEGER", nullable: false),
                    setor = table.Column<int>(type: "INTEGER", nullable: false),
                    solicitanteId = table.Column<int>(type: "INTEGER", nullable: false),
                    analistaId = table.Column<int>(type: "INTEGER", nullable: false),
                    categoriaId = table.Column<long>(type: "INTEGER", nullable: false),
                    comentarios = table.Column<string>(type: "TEXT", nullable: false),
                    dataAbertura = table.Column<DateTime>(type: "TEXT", nullable: false),
                    dataAtualizacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    dataFechamento = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chamados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chamados_Categorias_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chamados_Usuarios_analistaId",
                        column: x => x.analistaId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chamados_Usuarios_solicitanteId",
                        column: x => x.solicitanteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_analistaId",
                table: "Chamados",
                column: "analistaId");

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_categoriaId",
                table: "Chamados",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_solicitanteId",
                table: "Chamados",
                column: "solicitanteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chamados");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropColumn(
                name: "senha",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Usuarios",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Usuarios",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "perfil",
                table: "Usuarios",
                newName: "Ativo");
        }
    }
}
