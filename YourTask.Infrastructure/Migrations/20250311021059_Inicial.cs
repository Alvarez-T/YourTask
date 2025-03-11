using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YourTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tarefas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    DataConclusao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusTarefa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Tarefas",
                columns: new[] { "Id", "DataConclusao", "DataCriacao", "Descricao", "StatusTarefa", "Titulo" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comprar frutas, vegetais e laticínios.", 2, "Comprar mantimentos" },
                    { 2, null, new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reunião com a equipe de marketing.", 1, "Agendar reunião" },
                    { 3, null, new DateTime(2024, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Revisar conceitos de layout e binding.", 0, "Estudar WPF" },
                    { 4, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Enviar relatórios semanais.", 2, "Enviar e-mails" },
                    { 5, null, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Incluir os projetos recentes.", 1, "Atualizar portfólio" },
                    { 6, null, new DateTime(2024, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pesquisar destinos e reservar passagens.", 0, "Planejar viagem" },
                    { 7, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Terminar leitura do best-seller.", 2, "Ler livro" },
                    { 8, null, new DateTime(2024, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sessão de feedback com a equipe de desenvolvimento.", 1, "Reunião de feedback" },
                    { 9, new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Limpeza completa no escritório.", 2, "Limpeza geral" },
                    { 10, null, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Criar um protótipo funcional.", 0, "Desenvolver protótipo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarefas");
        }
    }
}
