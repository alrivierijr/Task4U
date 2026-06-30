using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Task4U.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelaEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "senha",
                table: "Usuario",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    inc_data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    inc_usuario_id = table.Column<int>(type: "integer", nullable: false),
                    alt_data = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    alt_usuario_id = table.Column<int>(type: "integer", nullable: false),
                    razao_social = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    nome_fantasia = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    inscricao_municipal = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    inscricao_estadual = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    logradouro = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    numero = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    complemento = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    bairro = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    cep = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    telefone = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    email = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    empresa_ativa = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.AlterColumn<string>(
                name: "senha",
                table: "Usuario",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200,
                oldNullable: true);
        }
    }
}
