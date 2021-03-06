using Microsoft.EntityFrameworkCore.Migrations;

namespace ssbbr.Migrations
{
    public partial class hamed3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NationalCode",
                table: "RegisterForm",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NationalCode",
                table: "RegisterForm",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 10);
        }
    }
}
