using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PurrfectBlog.Web.Migrations
{
  /// <inheritdoc />
  public partial class AddCategoryToBlogPost : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "BlogPosts",
          columns: table => new
          {
            Id = table.Column<int>(type: "INTEGER", nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Title = table.Column<string>(type: "TEXT", nullable: false),
            Content = table.Column<string>(type: "TEXT", nullable: false),
            Category = table.Column<string>(type: "TEXT", nullable: true),
            CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_BlogPosts", x => x.Id);
          });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "BlogPosts");
    }
  }
}