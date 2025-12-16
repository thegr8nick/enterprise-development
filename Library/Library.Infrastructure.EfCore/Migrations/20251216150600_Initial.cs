using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EditionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EditionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryNumber = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    AlphabetCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Authors = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    EditionTypeId = table.Column<int>(type: "int", nullable: false),
                    PublisherId = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_EditionTypes_EditionTypeId",
                        column: x => x.EditionTypeId,
                        principalTable: "EditionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookIssues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    ReaderId = table.Column<int>(type: "int", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookIssues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookIssues_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookIssues_Readers_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "Readers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "EditionTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Роман" },
                    { 2, "Повесть" },
                    { 3, "Учебник" },
                    { 4, "Справочник" },
                    { 5, "Фантастика" },
                    { 6, "Детектив" },
                    { 7, "Научная литература" },
                    { 8, "Сборник рассказов" },
                    { 9, "Историческая литература" },
                    { 10, "Документальная литература" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "АСТ" },
                    { 2, "Эксмо" },
                    { 3, "Просвещение" },
                    { 4, "Питер" },
                    { 5, "Наука" },
                    { 6, "Мир" },
                    { 7, "Олма" },
                    { 8, "Росмен" },
                    { 9, "Феникс" },
                    { 10, "Книжный Мир" }
                });

            migrationBuilder.InsertData(
                table: "Readers",
                columns: new[] { "Id", "Address", "FullName", "Phone", "RegistrationDate" },
                values: new object[,]
                {
                    { 1, "ул. Ленина, 10", "Иванов Иван Иванович", "89001001010", new DateTime(2023, 12, 16, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, "ул. Кирова, 22", "Петров Петр Петрович", "89002002020", new DateTime(2024, 12, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, "ул. Гагарина, 3", "Сидорова Анна Павловна", "89003003030", new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, "ул. Победы, 55", "Кузнецов Михаил Олегович", "89004004040", new DateTime(2025, 4, 16, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, "ул. Горького, 77", "Смирнова Ольга Николаевна", "89005005050", new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, "ул. Школьная, 8", "Васильев Дмитрий Андреевич", "89006006060", new DateTime(2025, 7, 16, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, "ул. Центральная, 1", "Попова Наталья Сергеевна", "89007007070", new DateTime(2025, 8, 16, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, "ул. Советская, 45", "Федоров Алексей Ильич", "89008008080", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, "ул. Молодежная, 12", "Алексеева Мария Петровна", "89009009090", new DateTime(2025, 10, 16, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, "ул. Парковая, 5", "Соколова Ксения Дмитриевна", "89001112233", new DateTime(2025, 11, 16, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AlphabetCode", "Authors", "EditionTypeId", "InventoryNumber", "PublisherId", "Title", "Year" },
                values: new object[,]
                {
                    { 1, "А-001", "А. Пушкин", 2, "INV-001", 1, "Капитанская дочка", 1836 },
                    { 2, "Т-145", "Л. Толстой", 1, "INV-002", 2, "Война и мир", 1869 },
                    { 3, "Д-019", "Ф. Достоевский", 1, "INV-003", 1, "Идиот", 1868 },
                    { 4, "П-120", "И. Тургенев", 1, "INV-004", 2, "Отцы и дети", 1862 },
                    { 5, "К-033", "Д. Лондон", 1, "INV-005", 7, "Мартин Иден", 1909 },
                    { 6, "О-220", "А. Азимов", 5, "INV-006", 10, "Основание", 1951 },
                    { 7, "С-001", "Р. Брэдбери", 5, "INV-007", 8, "451 градус по Фаренгейту", 1953 },
                    { 8, "У-115", "А. Кристи", 6, "INV-008", 9, "Убийство в Восточном экспрессе", 1934 },
                    { 9, "Ш-999", "А. Штраус", 7, "INV-009", 5, "Основы физики", 1999 },
                    { 10, "Г-008", "Д. Карнеги", 10, "INV-010", 4, "Как завоевывать друзей", 1936 }
                });

            migrationBuilder.InsertData(
                table: "BookIssues",
                columns: new[] { "Id", "BookId", "Days", "IssueDate", "ReaderId", "ReturnDate" },
                values: new object[,]
                {
                    { 1, 2, 14, new DateTime(2025, 12, 6, 0, 0, 0, 0, DateTimeKind.Utc), 1, null },
                    { 2, 2, 30, new DateTime(2025, 11, 6, 0, 0, 0, 0, DateTimeKind.Utc), 1, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 2, 21, new DateTime(2025, 9, 27, 0, 0, 0, 0, DateTimeKind.Utc), 1, new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 6, 30, new DateTime(2025, 8, 18, 0, 0, 0, 0, DateTimeKind.Utc), 1, new DateTime(2025, 9, 27, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 10, 60, new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Utc), 1, new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 1, 10, new DateTime(2025, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), 1, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 6, 14, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, null },
                    { 8, 6, 30, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Utc), 2, new DateTime(2025, 10, 17, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 2, 30, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Utc), 2, new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 5, 14, new DateTime(2025, 10, 17, 0, 0, 0, 0, DateTimeKind.Utc), 2, new DateTime(2025, 11, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, 10, 7, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc), 2, null },
                    { 12, 1, 7, new DateTime(2025, 11, 26, 0, 0, 0, 0, DateTimeKind.Utc), 3, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, 3, 14, new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc), 3, new DateTime(2025, 12, 6, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, 6, 30, new DateTime(2024, 11, 11, 0, 0, 0, 0, DateTimeKind.Utc), 3, new DateTime(2024, 12, 31, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 15, 8, 21, new DateTime(2025, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), 4, new DateTime(2025, 3, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 16, 5, 20, new DateTime(2025, 11, 13, 0, 0, 0, 0, DateTimeKind.Utc), 4, new DateTime(2025, 12, 11, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 17, 10, 14, new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Utc), 4, null },
                    { 18, 1, 10, new DateTime(2025, 12, 14, 0, 0, 0, 0, DateTimeKind.Utc), 5, null },
                    { 19, 5, 30, new DateTime(2025, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc), 5, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 20, 6, 30, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), 6, new DateTime(2025, 6, 9, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 21, 2, 10, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new DateTime(2025, 11, 16, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 22, 7, 7, new DateTime(2025, 11, 28, 0, 0, 0, 0, DateTimeKind.Utc), 7, new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 23, 8, 21, new DateTime(2024, 12, 21, 0, 0, 0, 0, DateTimeKind.Utc), 8, new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 24, 4, 14, new DateTime(2023, 10, 8, 0, 0, 0, 0, DateTimeKind.Utc), 9, new DateTime(2023, 10, 28, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 25, 9, 14, new DateTime(2025, 11, 6, 0, 0, 0, 0, DateTimeKind.Utc), 10, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 26, 10, 30, new DateTime(2025, 8, 18, 0, 0, 0, 0, DateTimeKind.Utc), 10, new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 27, 2, 7, new DateTime(2025, 12, 9, 0, 0, 0, 0, DateTimeKind.Utc), 10, null },
                    { 28, 1, 10, new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Utc), 2, new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 29, 3, 7, new DateTime(2025, 2, 19, 0, 0, 0, 0, DateTimeKind.Utc), 5, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { 30, 6, 14, new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Utc), 4, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookIssues_BookId",
                table: "BookIssues",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookIssues_ReaderId",
                table: "BookIssues",
                column: "ReaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AlphabetCode",
                table: "Books",
                column: "AlphabetCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_EditionTypeId",
                table: "Books",
                column: "EditionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_InventoryNumber",
                table: "Books",
                column: "InventoryNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherId",
                table: "Books",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Title",
                table: "Books",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_EditionTypes_Name",
                table: "EditionTypes",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Publishers_Name",
                table: "Publishers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Readers_FullName",
                table: "Readers",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_Readers_Phone",
                table: "Readers",
                column: "Phone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookIssues");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Readers");

            migrationBuilder.DropTable(
                name: "EditionTypes");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
