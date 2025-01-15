using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoTeste.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpToDateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "data_de_criacao",
                table: "pedido",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "pedido",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "pedido");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "data_de_criacao",
                table: "pedido",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }
    }
}