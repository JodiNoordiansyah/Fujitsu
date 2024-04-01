using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FujitsuWebApp.Migrations
{
    /// <inheritdoc />
    public partial class renametable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TbMCity",
                table: "TbMCity");

            migrationBuilder.RenameTable(
                name: "TbMCity",
                newName: "TB_M_CITY");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_M_CITY",
                table: "TB_M_CITY",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_M_CITY",
                table: "TB_M_CITY");

            migrationBuilder.RenameTable(
                name: "TB_M_CITY",
                newName: "TbMCity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TbMCity",
                table: "TbMCity",
                column: "Id");
        }
    }
}
