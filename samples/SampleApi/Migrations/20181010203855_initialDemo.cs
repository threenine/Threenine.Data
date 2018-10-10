using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleApi.Migrations
{
    public partial class initialDemo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 10, nullable: true),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Profile = table.Column<string>(nullable: true),
                    TagLine = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Profile", "TagLine", "Title" },
                values: new object[] { 1, "foo@foo.com", "Foo", "Bar", "Foo Bar Bar Foo", "FooBar", "Mr" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Profile", "TagLine", "Title" },
                values: new object[] { 2, "foo2@foo2.com", "Foo2", "Bar2", "Foo Bar Bar Foo2", "FooBar2", "Mr" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Profile", "TagLine", "Title" },
                values: new object[] { 3, "foo3@foo3.com", "Foo3", "Bar3", "Foo Bar Bar Foo3", "FooBar3", "Mr" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
