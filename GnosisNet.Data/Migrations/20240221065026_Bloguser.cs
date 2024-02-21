using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GnosisNet.Data.Migrations
{
    /// <inheritdoc />
    public partial class Bloguser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "updatedby",
                table: "Blogs",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "createdby",
                table: "Blogs",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_createdby",
                table: "Blogs",
                column: "createdby");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_AspNetUsers_createdby",
                table: "Blogs",
                column: "createdby",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_AspNetUsers_createdby",
                table: "Blogs");

            migrationBuilder.DropIndex(
                name: "IX_Blogs_createdby",
                table: "Blogs");

            migrationBuilder.AlterColumn<string>(
                name: "updatedby",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "createdby",
                table: "Blogs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);
        }
    }
}
