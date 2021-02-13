using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class GuidMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                    Login = table.Column<string>("nvarchar(max)", nullable: true),
                    Password = table.Column<string>("nvarchar(max)", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

            migrationBuilder.CreateTable(
                "BankingAccount",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Money = table.Column<decimal>("decimal(18,2)", nullable: false),
                    State = table.Column<bool>("bit", nullable: false),
                    UserId = table.Column<Guid>("uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankingAccount", x => x.Id);
                    table.ForeignKey(
                        "FK_BankingAccount_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Profile",
                table => new
                {
                    Id = table.Column<int>("int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>("nvarchar(max)", nullable: true),
                    Age = table.Column<int>("int", nullable: false),
                    UserId = table.Column<Guid>("uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Id);
                    table.ForeignKey(
                        "FK_Profile_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_BankingAccount_UserId",
                "BankingAccount",
                "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_Profile_UserId",
                "Profile",
                "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "BankingAccount");

            migrationBuilder.DropTable(
                "Profile");

            migrationBuilder.DropTable(
                "Users");
        }
    }
}