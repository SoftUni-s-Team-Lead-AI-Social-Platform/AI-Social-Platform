using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Social_Platform.Data.Migrations
{
    public partial class publicationmediarelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("000bf091-aab7-4b90-9ced-da755a3f6e71"));

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("0345340c-c108-4e78-a0ea-f5077e57c78c"));

            migrationBuilder.AddColumn<Guid>(
                name: "PublicationId",
                table: "MediaFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e681e14d-59fb-4f0c-a2e1-bbccff2c6002", "AQAAAAEAACcQAAAAEOJnMR4QOCrZDuZU7uHJfgeEaEYtfaYlnmZTXXB0pS6j7BnqI4lSfgf/zPaGcFhnjQ==", "09b12802-e007-4590-8f1f-c8d2afffcaca" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ed828a9a-4bd2-43ef-9e0e-0c67863dbd0b", "AQAAAAEAACcQAAAAEDiVnynwuWFc3vRtrLHV4LmE1zE4k0ZRdNt6z7wp+pNfrDGh4EqwQ7Qiff0hV3b2BQ==", "91cd5bba-4890-4c84-822a-fd5304744e6a" });

            migrationBuilder.InsertData(
                table: "Publications",
                columns: new[] { "Id", "AuthorId", "Content", "DateCreated" },
                values: new object[,]
                {
                    { new Guid("6c203897-6177-4e14-93ea-d5deb2f07329"), new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"), "This is the first seeded publication Content from Ivan", new DateTime(2023, 11, 28, 15, 4, 43, 105, DateTimeKind.Utc).AddTicks(6493) },
                    { new Guid("7dfdb8e2-ec27-417c-8a19-978812d71ed1"), new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"), "This is the second seeded publication Content from Georgi", new DateTime(2023, 11, 28, 15, 4, 43, 105, DateTimeKind.Utc).AddTicks(6514) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MediaFiles_PublicationId",
                table: "MediaFiles",
                column: "PublicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MediaFiles_Publications_PublicationId",
                table: "MediaFiles",
                column: "PublicationId",
                principalTable: "Publications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MediaFiles_Publications_PublicationId",
                table: "MediaFiles");

            migrationBuilder.DropIndex(
                name: "IX_MediaFiles_PublicationId",
                table: "MediaFiles");

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("6c203897-6177-4e14-93ea-d5deb2f07329"));

            migrationBuilder.DeleteData(
                table: "Publications",
                keyColumn: "Id",
                keyValue: new Guid("7dfdb8e2-ec27-417c-8a19-978812d71ed1"));

            migrationBuilder.DropColumn(
                name: "PublicationId",
                table: "MediaFiles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f1c84d6-02a3-407a-8130-6573216a4348", "AQAAAAEAACcQAAAAEKp1BcOYnZQsSlc+MQhYawUge9D+OayyKAZKaFn0uTbC0NgzQg0gjNCIQwX3Q4GROQ==", "b3fd3689-7d22-4939-8168-54fe750782a7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b72623c-f588-48ad-a610-07f38abf7e89", "AQAAAAEAACcQAAAAEEgJLS7gNJhFhf6mOXkd8NhZl6M3MgeTEk++VBpXM5tFlTm80XfKfqaoQHicCOg8gw==", "9ecb28ab-b1a3-46ed-b63a-57db8fa84ffe" });

            migrationBuilder.InsertData(
                table: "Publications",
                columns: new[] { "Id", "AuthorId", "Content", "DateCreated" },
                values: new object[,]
                {
                    { new Guid("000bf091-aab7-4b90-9ced-da755a3f6e71"), new Guid("6d5800ce-d726-4fc8-83d9-d6b3ac1f591e"), "This is the second seeded publication Content from Georgi", new DateTime(2023, 11, 26, 3, 27, 31, 882, DateTimeKind.Utc).AddTicks(7113) },
                    { new Guid("0345340c-c108-4e78-a0ea-f5077e57c78c"), new Guid("949a14ed-2e82-4f5a-a684-a9c7e3ccb52e"), "This is the first seeded publication Content from Ivan", new DateTime(2023, 11, 26, 3, 27, 31, 882, DateTimeKind.Utc).AddTicks(7101) }
                });
        }
    }
}
