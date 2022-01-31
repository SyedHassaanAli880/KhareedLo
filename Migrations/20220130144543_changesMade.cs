using Microsoft.EntityFrameworkCore.Migrations;

namespace KhareedLo.Migrations
{
    public partial class changesMade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "ReviewAndComments");

            //migrationBuilder.DropColumn(
            //    name: "Category",
            //    table: "Products");

            //migrationBuilder.RenameColumn(
            //    name: "quantity",
            //    table: "Products",
            //    newName: "Quantity");

            //migrationBuilder.RenameColumn(
            //    name: "FeedbackID",
            //    table: "Feedbacks",
            //    newName: "FeedbackId");

            //migrationBuilder.AddColumn<int>(
            //    name: "CategoryID",
            //    table: "Products",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "CategoryNameId",
            //    table: "Products",
            //    type: "int",
            //    nullable: true);

            //migrationBuilder.CreateTable(
            //    name: "ReviewsAndComments",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ProductId = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ReviewsAndComments", x => x.Id);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Products_CategoryNameId",
            //    table: "Products",
            //    column: "CategoryNameId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Products_CategoryModels_CategoryNameId",
            //    table: "Products",
            //    column: "CategoryNameId",
            //    principalTable: "CategoryModels",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CategoryModels_CategoryNameId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ReviewsAndComments");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryNameId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryNameId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Products",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "FeedbackId",
                table: "Feedbacks",
                newName: "FeedbackID");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReviewAndComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewAndComments", x => x.Id);
                });
        }
    }
}
