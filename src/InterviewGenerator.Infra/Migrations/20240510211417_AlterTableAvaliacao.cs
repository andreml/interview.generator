using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InterviewGenerator.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableAvaliacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataAplicacao",
                table: "Avaliacao",
                newName: "DataEnvio");

            migrationBuilder.AlterColumn<decimal>(
                name: "Nota",
                table: "Avaliacao",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataResposta",
                table: "Avaliacao",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Respondida",
                table: "Avaliacao",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataResposta",
                table: "Avaliacao");

            migrationBuilder.DropColumn(
                name: "Respondida",
                table: "Avaliacao");

            migrationBuilder.RenameColumn(
                name: "DataEnvio",
                table: "Avaliacao",
                newName: "DataAplicacao");

            migrationBuilder.AlterColumn<decimal>(
                name: "Nota",
                table: "Avaliacao",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);
        }
    }
}
