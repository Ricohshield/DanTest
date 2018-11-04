using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DanTest.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchRequests",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Term = table.Column<string>(nullable: true),
                    ForkFrom = table.Column<int>(nullable: true),
                    ForkTo = table.Column<int>(nullable: true),
                    Language = table.Column<int>(nullable: false),
                    StarsFrom = table.Column<int>(nullable: true),
                    StarsTo = table.Column<int>(nullable: true),
                    SizeFrom = table.Column<int>(nullable: true),
                    SizeTo = table.Column<int>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repositories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GitHubId = table.Column<long>(nullable: false),
                    SearchRequestId = table.Column<long>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StarCount = table.Column<int>(nullable: false),
                    ForkCount = table.Column<int>(nullable: false),
                    AuthorName = table.Column<string>(nullable: true),
                    AuthorImg = table.Column<string>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: false),
                    Language = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repositories_SearchRequests_SearchRequestId",
                        column: x => x.SearchRequestId,
                        principalTable: "SearchRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Repositories_SearchRequestId",
                table: "Repositories",
                column: "SearchRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Repositories");

            migrationBuilder.DropTable(
                name: "SearchRequests");
        }
    }
}
