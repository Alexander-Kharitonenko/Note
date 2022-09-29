using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes.Persistence.MySQL.Migrations
{
    public partial class addCompletePropertyToNotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(name: "IsCmpleted", table: "notes", type: "bool", nullable: false);

            migrationBuilder.Sql("UPDATE notes SET IsCmpleted=false");
    
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");
        }
    }
}
