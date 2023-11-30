using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moviegenras_Movies_MovieId",
                table: "Moviegenras");

            migrationBuilder.DropForeignKey(
                name: "FK_Moviegenras_genras_genraId",
                table: "Moviegenras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Moviegenras",
                table: "Moviegenras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_genras",
                table: "genras");

            migrationBuilder.RenameTable(
                name: "Moviegenras",
                newName: "MovieGenras");

            migrationBuilder.RenameTable(
                name: "genras",
                newName: "Genras");

            migrationBuilder.RenameColumn(
                name: "genraId",
                table: "MovieGenras",
                newName: "GenraId");

            migrationBuilder.RenameIndex(
                name: "IX_Moviegenras_genraId",
                table: "MovieGenras",
                newName: "IX_MovieGenras_GenraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieGenras",
                table: "MovieGenras",
                columns: new[] { "MovieId", "GenraId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genras",
                table: "Genras",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenras_Genras_GenraId",
                table: "MovieGenras",
                column: "GenraId",
                principalTable: "Genras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenras_Movies_MovieId",
                table: "MovieGenras",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenras_Genras_GenraId",
                table: "MovieGenras");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenras_Movies_MovieId",
                table: "MovieGenras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieGenras",
                table: "MovieGenras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genras",
                table: "Genras");

            migrationBuilder.RenameTable(
                name: "MovieGenras",
                newName: "Moviegenras");

            migrationBuilder.RenameTable(
                name: "Genras",
                newName: "genras");

            migrationBuilder.RenameColumn(
                name: "GenraId",
                table: "Moviegenras",
                newName: "genraId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGenras_GenraId",
                table: "Moviegenras",
                newName: "IX_Moviegenras_genraId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Moviegenras",
                table: "Moviegenras",
                columns: new[] { "MovieId", "genraId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_genras",
                table: "genras",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Moviegenras_Movies_MovieId",
                table: "Moviegenras",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Moviegenras_genras_genraId",
                table: "Moviegenras",
                column: "genraId",
                principalTable: "genras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
