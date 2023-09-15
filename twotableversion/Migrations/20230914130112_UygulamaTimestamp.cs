﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace twotableversion.Migrations
{
    /// <inheritdoc />
    public partial class UygulamaTimestamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Uygulamalar",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Uygulamalar");
        }
    }
}
