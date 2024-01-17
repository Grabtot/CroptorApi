using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Croptor.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPresetNameIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Presets_UserId",
                table: "Presets");

            migrationBuilder.DropColumn(
                name: "CustomSizesId",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_Presets_UserId_Name",
                table: "Presets",
                columns: new[] { "UserId", "Name" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Presets_UserId_Name",
                table: "Presets");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomSizesId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Presets_UserId",
                table: "Presets",
                column: "UserId");
        }
    }
}
