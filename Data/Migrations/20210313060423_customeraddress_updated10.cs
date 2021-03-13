using Microsoft.EntityFrameworkCore.Migrations;

namespace Boot_Factory.Data.Migrations
{
    public partial class customeraddress_updated10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerAddress",
                table: "Sales");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerAddress",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
