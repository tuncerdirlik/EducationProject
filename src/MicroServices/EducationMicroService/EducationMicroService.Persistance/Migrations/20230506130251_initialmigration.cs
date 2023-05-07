using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EducationMicroService.Persistence.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "education",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    education_group_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    link = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    modified_date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_education", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "education");
        }
    }
}
