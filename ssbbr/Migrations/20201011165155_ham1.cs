using Microsoft.EntityFrameworkCore.Migrations;

namespace ssbbr.Migrations
{
    public partial class ham1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Province",
                table: "RegisterForm",
                newName: "استان");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "RegisterForm",
                newName: "تلفن ثابت");

            migrationBuilder.RenameColumn(
                name: "Mobile",
                table: "RegisterForm",
                newName: "موبایل");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "RegisterForm",
                newName: "نام خانوادگی");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "RegisterForm",
                newName: "نام");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "RegisterForm",
                newName: "ایمیل");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "RegisterForm",
                newName: "شهر");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "استان",
                table: "RegisterForm",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "تلفن ثابت",
                table: "RegisterForm",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "موبایل",
                table: "RegisterForm",
                newName: "Mobile");

            migrationBuilder.RenameColumn(
                name: "نام خانوادگی",
                table: "RegisterForm",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "نام",
                table: "RegisterForm",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "ایمیل",
                table: "RegisterForm",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "شهر",
                table: "RegisterForm",
                newName: "City");
        }
    }
}
