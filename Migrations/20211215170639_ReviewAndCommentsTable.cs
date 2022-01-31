using Microsoft.EntityFrameworkCore.Migrations;

namespace KhareedLo.Migrations
{
    public partial class ReviewAndCommentsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AlterColumn<int>(
            //    name: "IsActive",
            //    table: "CategoryModels",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(short),
            //    oldType: "smallint");

            //migrationBuilder.CreateTable(
            //    name: "ReviewAndComments",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ReviewAndComments", x => x.Id);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewAndComments");

            migrationBuilder.AlterColumn<short>(
                name: "IsActive",
                table: "CategoryModels",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
