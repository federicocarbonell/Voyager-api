using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VoyageAPI.Migrations
{
    public partial class UpdateReportsentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArriveTime",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "Report");

            migrationBuilder.RenameColumn(
                name: "LeaveTime",
                table: "Report",
                newName: "Detail");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Report",
                newName: "Comment");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Report",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeArrival",
                table: "Report",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeResolution",
                table: "Report",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "VisitDate",
                table: "Report",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Report",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Report_ProductId",
                table: "Report",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_ReportId",
                table: "Image",
                column: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Product_ProductId",
                table: "Report",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Report_Product_ProductId",
                table: "Report");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropIndex(
                name: "IX_Report_ProductId",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "TimeArrival",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "TimeResolution",
                table: "Report");

            migrationBuilder.DropColumn(
                name: "VisitDate",
                table: "Report");

            migrationBuilder.RenameColumn(
                name: "Detail",
                table: "Report",
                newName: "LeaveTime");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Report",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "ArriveTime",
                table: "Report",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Report",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Report",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
