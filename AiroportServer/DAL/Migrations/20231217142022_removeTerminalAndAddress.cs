using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class removeTerminalAndAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Address_ArrivalAddressId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Address_DepartureAddressId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Gates_GateId",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Gates");

            migrationBuilder.DropIndex(
                name: "IX_Flights_ArrivalAddressId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_DepartureAddressId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "ArrivalAddressId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DepartureAddressId",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "GateId",
                table: "Flights",
                newName: "TerminalId");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_GateId",
                table: "Flights",
                newName: "IX_Flights_TerminalId");

            migrationBuilder.AddColumn<string>(
                name: "ArrivalAddress",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DepartureAddress",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Terminals_TerminalId",
                table: "Flights",
                column: "TerminalId",
                principalTable: "Terminals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Terminals_TerminalId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "ArrivalAddress",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "DepartureAddress",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "TerminalId",
                table: "Flights",
                newName: "GateId");

            migrationBuilder.RenameIndex(
                name: "IX_Flights_TerminalId",
                table: "Flights",
                newName: "IX_Flights_GateId");

            migrationBuilder.AddColumn<int>(
                name: "ArrivalAddressId",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartureAddressId",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TerminalId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gates_Terminals_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Terminals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_ArrivalAddressId",
                table: "Flights",
                column: "ArrivalAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_DepartureAddressId",
                table: "Flights",
                column: "DepartureAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Gates_TerminalId",
                table: "Gates",
                column: "TerminalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Address_ArrivalAddressId",
                table: "Flights",
                column: "ArrivalAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Address_DepartureAddressId",
                table: "Flights",
                column: "DepartureAddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Gates_GateId",
                table: "Flights",
                column: "GateId",
                principalTable: "Gates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
