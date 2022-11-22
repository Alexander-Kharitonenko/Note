using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes.Persistence.MySQL.Migrations
{
    public partial class renameColumnNoteIsComplete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCmpleted",
                table: "Notes",
                newName: "IsCompleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCompleted",
                table: "Notes",
                newName: "IsCmpleted");
        }
    }
}
