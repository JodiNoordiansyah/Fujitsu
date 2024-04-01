using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FujitsuWebApp.Migrations
{
    /// <inheritdoc />
    public partial class addMasterCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "TB_M_SUPPLIER",
            //    columns: table => new
            //    {
            //        SUPPLIER_CODE = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
            //        SUPPLIER_NAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        ADDRESS = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
            //        PROVINCE = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        CITY = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
            //        PIC = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TB_M_SUPPLIER_SUPPLIER_CODE", x => x.SUPPLIER_CODE);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TB_R_ORDER_H",
            //    columns: table => new
            //    {
            //        ORDER_NO = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
            //        ORDER_DATE = table.Column<DateOnly>(type: "date", nullable: true),
            //        SUPPLIER_CODE = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
            //        AMOUNT = table.Column<decimal>(type: "numeric(18,0)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TB_R_ORDER_H_ORDER_NO", x => x.ORDER_NO);
            //    });

            migrationBuilder.CreateTable(
                name: "TbMCity",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbMCity", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "TB_M_SUPPLIER");

            //migrationBuilder.DropTable(
            //    name: "TB_R_ORDER_H");

            migrationBuilder.DropTable(
                name: "TbMCity");
        }
    }
}
