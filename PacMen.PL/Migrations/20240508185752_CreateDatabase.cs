using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PacMen.PL.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblScore",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    tblUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblScore_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ScoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser_Id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_tblUser_ScoreId",
                        column: x => x.ScoreId,
                        principalTable: "tblScore",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "tblScore",
                columns: new[] { "Id", "Date", "Level", "Score", "tblUserId" },
                values: new object[,]
                {
                    { new Guid("58d1570c-443c-4d7a-86a6-78eb095c79fe"), new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 100, 100, null },
                    { new Guid("8934b691-1ad3-4807-b605-f51b094d8ee7"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 66, null },
                    { new Guid("da0d4d68-0e1f-454d-bb85-8284cd1224c6"), new DateTime(2024, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 50, null }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "Email", "FirstName", "Image", "LastName", "Password", "ScoreId", "UserName" },
                values: new object[,]
                {
                    { new Guid("015d5a3d-dd7d-42a3-89b1-eb9ab81fe014"), "arosas@gmail.com", "Alex", "arosas", "Rosas", "0VjICkwiRKf334OSi0HtMV+i7IY=", new Guid("da0d4d68-0e1f-454d-bb85-8284cd1224c6"), "arosas" },
                    { new Guid("a5204e01-262c-471a-a195-3a5055ded623"), "user1.com", "User1", "user.img", "us", "lclGv2Iu+TsKIRzQ/QKN/fz3454=", new Guid("8934b691-1ad3-4807-b605-f51b094d8ee7"), "user1" },
                    { new Guid("c494289f-3ff3-45a9-ad4f-cda3ec035c1a"), "user2.com", "User2", "user2.img", "us2", "j7XP6SJnTg+fqkapJxb2a9Z600Q=", new Guid("58d1570c-443c-4d7a-86a6-78eb095c79fe"), "user2" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblScore_tblUserId",
                table: "tblScore",
                column: "tblUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUser_ScoreId",
                table: "tblUser",
                column: "ScoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblScore_tblUser_tblUserId",
                table: "tblScore",
                column: "tblUserId",
                principalTable: "tblUser",
                principalColumn: "Id");

            migrationBuilder.Sql(@"CREATE PROCEDURE [dbo].[spGetScores]
                AS
	                select s.Id, s.Score, s.Date, s.Level
                    from tblScore s
                RETURN 0");

            migrationBuilder.Sql(@"CREATE PROCEDURE [dbo].[spGetUsers]
                AS
	                select u.Id, u.FirstName, u.LastName, u.Email, u.UserName, u.Image, u.Password,
                    u.ScoreId
                    from tblUser u
                    inner join tblScore s on u.ScoreId = s.Id
                RETURN 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblScore_tblUser_tblUserId",
                table: "tblScore");

            migrationBuilder.DropTable(
                name: "tblUser");

            migrationBuilder.DropTable(
                name: "tblScore");
        }
    }
}
