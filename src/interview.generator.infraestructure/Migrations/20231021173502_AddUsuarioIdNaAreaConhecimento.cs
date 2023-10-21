using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace interview.generator.infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioIdNaAreaConhecimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "AreaConhecimento",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "AreaConhecimento");
        }
    }
}
