using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Social_Platform.Data.Migrations
{
    public partial class changeUserToAuthorIdOnPublication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_AspNetUsers_UserId",
                table: "Publications");

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("6afd813d-9691-4db9-ad51-3fccbe12fcc6"));

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("fba66b78-42be-4b7e-898c-5b9d67891d12"));

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Publications",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Publications_UserId",
                table: "Publications",
                newName: "IX_Publications_AuthorId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9788a5ff-cd54-4cbc-866b-97893d901c7b", "AQAAAAEAACcQAAAAEMyfaJ0qpwZOAcRD7Uq8rdUgIre1AdvjhqUHq7XY6E9B6Vrcz1gs2TxxNmAflNUhow==", "cf9a03b9-fe49-4ec5-b36e-20e5bc4a8bd3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "eb90ed0e-d852-40ed-bb38-cba016742bb7", "AQAAAAEAACcQAAAAEEbIxcC8x1vIq7ts0KrCP8pDd0cfgY3FEVjaMBGdS1/BCqwL23sM2yfJLSFg90mJWQ==", "0d1035f0-81fc-4ad7-b3ef-5ca631f54e89" });

            migrationBuilder.InsertData(
                table: "Publications",
                columns: new[] { "Id", "AuthorId", "Content", "DateCreated" },
                values: new object[,]
                {
                    { new Guid("4721fd66-6de2-4f83-b363-9ac1896d3d48"), new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"), "This is the first seeded publication Content from Ivan", new DateTime(2023, 12, 2, 1, 27, 19, 925, DateTimeKind.Utc).AddTicks(1571) },
                    { new Guid("c4976cf2-7320-44a5-bf0b-c7868862499e"), new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"), "This is the second seeded publication Content from Georgi", new DateTime(2023, 12, 2, 1, 27, 19, 925, DateTimeKind.Utc).AddTicks(1591) }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_AspNetUsers_AuthorId",
                table: "Publications",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_AspNetUsers_AuthorId",
                table: "Publications");

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("4721fd66-6de2-4f83-b363-9ac1896d3d48"));

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("c4976cf2-7320-44a5-bf0b-c7868862499e"));

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Publications",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Publications_AuthorId",
                table: "Publications",
                newName: "IX_Publications_UserId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7341e9e6-246b-4009-86ce-a4e078416236", "AQAAAAEAACcQAAAAELvigob8j3bXFbGu8Ap/0UoZpbCtyxNtqH7kw17t69s9yauy6UOGDgo2fDrZZpWZcg==", "2b6644ce-4198-4591-ae97-68a5f7b00853" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba016937-5ff0-4a12-8218-2d4dab731f8f", "AQAAAAEAACcQAAAAEA921uay8WI0HvHGZfkVEsdUXfe4I5JLGboCLjq+/CuW6m1wcRloaThz0i+2n2jHpQ==", "5427adc3-6950-4a1c-b393-e027486010ef" });

            migrationBuilder.InsertData(
                table: "Publications",
                columns: new[] { "Id", "Content", "DateCreated", "UserId" },
                values: new object[,]
                {
                    { new Guid("6afd813d-9691-4db9-ad51-3fccbe12fcc6"), "This is the first seeded publication Content from Ivan", new DateTime(2023, 12, 2, 0, 3, 2, 429, DateTimeKind.Utc).AddTicks(9165), new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e") },
                    { new Guid("fba66b78-42be-4b7e-898c-5b9d67891d12"), "This is the second seeded publication Content from Georgi", new DateTime(2023, 12, 2, 0, 3, 2, 429, DateTimeKind.Utc).AddTicks(9219), new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_AspNetUsers_UserId",
                table: "Publications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
