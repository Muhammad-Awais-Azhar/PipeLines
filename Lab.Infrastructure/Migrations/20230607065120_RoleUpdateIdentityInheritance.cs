using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SVX.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RoleUpdateIdentityInheritance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "Roles",
                newName: "NormalizedName");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "NormalizedName",
                table: "Roles",
                newName: "UpdatedBy");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "RoleName", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "System", new DateTime(2023, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), "Administrator", null, null },
                    { 2, "System", new DateTime(2023, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), "Owner", null, null },
                    { 3, "System", new DateTime(2023, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), "Guest", null, null },
                    { 4, "System", new DateTime(2023, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), "Issuer", null, null },
                    { 5, "System", new DateTime(2023, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), "Community Member", null, null },
                    { 6, "System", new DateTime(2023, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), "Ecosystem Partner", null, null },
                    { 7, "System", new DateTime(2023, 6, 7, 0, 0, 0, 0, DateTimeKind.Local), "Anchor Partner", null, null }
                });
        }
    }
}
