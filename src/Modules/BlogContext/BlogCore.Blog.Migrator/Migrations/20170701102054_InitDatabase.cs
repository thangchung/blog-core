using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogCore.Blog.Migrator.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "blog");

            migrationBuilder.CreateTable(
                name: "Blogs",
                schema: "blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DaysToComment = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    ModerateComments = table.Column<bool>(nullable: false),
                    OwnerEmail = table.Column<string>(nullable: true),
                    PostsPerPage = table.Column<int>(nullable: false),
                    Theme = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostIds",
                schema: "blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BlogId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostIds_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalSchema: "blog",
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostIds_BlogId",
                schema: "blog",
                table: "PostIds",
                column: "BlogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostIds",
                schema: "blog");

            migrationBuilder.DropTable(
                name: "Blogs",
                schema: "blog");
        }
    }
}
