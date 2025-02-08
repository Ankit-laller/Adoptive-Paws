using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdoptivePaws.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PetTableAddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    SNo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    petId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    petName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    petAge = table.Column<int>(type: "int", nullable: false),
                    isAdopted = table.Column<bool>(type: "bit", nullable: false),
                    vaccinated = table.Column<bool>(type: "bit", nullable: false),
                    petGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    petType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<int>(type: "int", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.SNo);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PetEntitySNo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Pets_PetEntitySNo",
                        column: x => x.PetEntitySNo,
                        principalTable: "Pets",
                        principalColumn: "SNo");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_PetEntitySNo",
                table: "Image",
                column: "PetEntitySNo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Pets");
        }
    }
}
