using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Uku.Database.Migrations
{
    public partial class CreateAlbum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    AlbumId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Barcode = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Top3kPosition = table.Column<int>(nullable: true),
                    Year = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => x.AlbumId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Album");
        }
    }
}
