using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Contact.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Foto = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacto_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "Deleted", "DeletedAt", "DeletedById", "Enabled", "Password", "UpdatedAt", "UpdatedById", "User" },
                values: new object[,]
                {
                    { new Guid("1128630a-da70-4242-8a56-ff0b7d38dc1d"), new DateTime(2023, 12, 31, 7, 55, 17, 403, DateTimeKind.Local).AddTicks(1466), new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, true, "1EC565DA06E86121A4FA61DFA4627AF44CB2B6C949EB208859AA42D2DD76A343", null, null, "dtigua" },
                    { new Guid("43aeeeda-9961-4047-9a6c-fb40aed780cc"), new DateTime(2023, 12, 31, 7, 55, 17, 403, DateTimeKind.Local).AddTicks(1452), new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, true, "0598BF5B847F0297328C2F6F77CEC86C41DF1AD704FCE9B429CAC28019FEA76B", null, null, "ayoung" },
                    { new Guid("5ef58fa0-e108-4d8c-aa1e-8640813deb15"), new DateTime(2023, 12, 31, 7, 55, 17, 403, DateTimeKind.Local).AddTicks(1469), new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, true, "2D0BB132CDF675AB1EC10806D332A85E9AEF84C4F73A2214D5318BC6C5905210", null, null, "mcastro" }
                });

            migrationBuilder.InsertData(
                table: "Contacto",
                columns: new[] { "Id", "Apellido", "Celular", "CreatedAt", "CreatedById", "Deleted", "DeletedAt", "DeletedById", "Enabled", "Foto", "Nombre", "Telefono", "UpdatedAt", "UpdatedById", "UsuarioId" },
                values: new object[,]
                {
                    { new Guid("1489cbe9-d763-434e-ba9e-1149555e35db"), "Tigua", "+593977689656", new DateTime(2023, 12, 31, 7, 55, 17, 402, DateTimeKind.Local).AddTicks(8118), new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, true, null, "Danna", "02607739", null, null, new Guid("1128630a-da70-4242-8a56-ff0b7d38dc1d") },
                    { new Guid("52a69d0a-3017-42c2-ad61-0a89237ce452"), "Castro", "+593964489656", new DateTime(2023, 12, 31, 7, 55, 17, 402, DateTimeKind.Local).AddTicks(8123), new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, true, null, "Milena", "02688639", null, null, new Guid("5ef58fa0-e108-4d8c-aa1e-8640813deb15") },
                    { new Guid("f3c1d434-ba7e-4fc9-be72-dbdd3901bd09"), "Young", "+593963689656", new DateTime(2023, 12, 31, 7, 55, 17, 402, DateTimeKind.Local).AddTicks(8081), new Guid("00000000-0000-0000-0000-000000000000"), false, null, null, true, null, "Alaska", "02609639", null, null, new Guid("43aeeeda-9961-4047-9a6c-fb40aed780cc") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacto_UsuarioId",
                table: "Contacto",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacto");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
