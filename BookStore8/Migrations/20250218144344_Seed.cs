using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore8.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e891c19-3594-4362-82b1-e8b7c750173f", null, "User", "USER" },
                    { "560dbc73-4df3-4494-9d1f-4d2e44cb31b9", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0a56a833-809f-441d-bbfd-7efb819d0671", 0, "f0969671-c212-43ca-86d1-b54804c64772", "admin@booksore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAIAAYagAAAAED1cUWcmmoRguiP4U8CeVaoJhoScRLMozuNiPd+iTbxz+kJ6fBlNHE0Jiywxs/rTwQ==", null, false, "9de5f4e8-8f52-45ce-b09f-9be0bfcb868d", false, "admin@booksore.com" },
                    { "0f51be92-235f-4c90-857b-d90e07eaae9b", 0, "7b929c25-b366-4432-8bf6-fbb01b609211", "user@booksore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAIAAYagAAAAECXFDaTAO/Akdgy6aV0kpmmFduHQqktXhfEdBwYVOdSWKGH4+ISs5dm2sNsXM/m9kA==", null, false, "235539c5-33dc-4070-b42f-9e985c41adac", false, "user@booksore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "560dbc73-4df3-4494-9d1f-4d2e44cb31b9", "0a56a833-809f-441d-bbfd-7efb819d0671" },
                    { "2e891c19-3594-4362-82b1-e8b7c750173f", "0f51be92-235f-4c90-857b-d90e07eaae9b" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "560dbc73-4df3-4494-9d1f-4d2e44cb31b9", "0a56a833-809f-441d-bbfd-7efb819d0671" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2e891c19-3594-4362-82b1-e8b7c750173f", "0f51be92-235f-4c90-857b-d90e07eaae9b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e891c19-3594-4362-82b1-e8b7c750173f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "560dbc73-4df3-4494-9d1f-4d2e44cb31b9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0a56a833-809f-441d-bbfd-7efb819d0671");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0f51be92-235f-4c90-857b-d90e07eaae9b");
        }
    }
}
