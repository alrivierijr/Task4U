using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Task4U.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    inc_data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    inc_usuario_id = table.Column<int>(type: "integer", nullable: false),
                    alt_data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    alt_usuario_id = table.Column<int>(type: "integer", nullable: false),
                    nome = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    email = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    senha = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ultimo_acesso_data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ultimo_acesso_ip = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    nivel_acesso = table.Column<int>(type: "integer", nullable: false),
                    usuario_ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
