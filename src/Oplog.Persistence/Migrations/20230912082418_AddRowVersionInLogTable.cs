using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oplog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRowVersionInLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Logs",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Logs");
        }
    }
}
