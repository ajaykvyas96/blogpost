using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GnosisNet.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewTblBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    postbody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    publisheddate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<int>(type: "int", nullable: false),
                    createdon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    createdby = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    updatedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    updatedon = table.Column<DateTime>(type: "datetime2", nullable: true),
                    isactive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}
