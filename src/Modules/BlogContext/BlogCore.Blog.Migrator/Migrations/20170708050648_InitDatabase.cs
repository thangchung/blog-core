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
                name: "BlogSettings",
                schema: "blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DaysToComment = table.Column<int>(nullable: false),
                    ModerateComments = table.Column<bool>(nullable: false),
                    PostsPerPage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                schema: "blog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BlogSettingId = table.Column<Guid>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageFilePath = table.Column<string>(nullable: false),
                    OwnerEmail = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Theme = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_BlogSettings_BlogSettingId",
                        column: x => x.BlogSettingId,
                        principalSchema: "blog",
                        principalTable: "BlogSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_Blogs_BlogSettingId",
                schema: "blog",
                table: "Blogs",
                column: "BlogSettingId");

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

            migrationBuilder.DropTable(
                name: "BlogSettings",
                schema: "blog");
        }
    }
}
