using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectName.Application.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditEntry",
                columns: table => new
                {
                    AuditEntryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditEvent = table.Column<byte>(type: "TINYINT", nullable: false),
                    AuditedEntityType = table.Column<string>(type: "varchar(512)", nullable: true),
                    AuditedEntityId = table.Column<long>(type: "BIGINT", nullable: true),
                    AuditDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    InitiatedBy = table.Column<string>(type: "varchar(256)", nullable: true),
                    PreviousState = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    NewState = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    ReferenceId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    DataState = table.Column<byte>(type: "TINYINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEntry", x => x.AuditEntryId);
                });

            migrationBuilder.CreateTable(
                name: "TodoList",
                columns: table => new
                {
                    TodoListId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    ReferenceId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    State = table.Column<byte>(type: "TINYINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoList", x => x.TodoListId);
                });

            migrationBuilder.CreateTable(
                name: "TodoItem",
                columns: table => new
                {
                    TodoItemId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TodoListId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    Note = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true),
                    Priority = table.Column<byte>(type: "tinyint", nullable: false),
                    ReminderDate = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: true),
                    CreateddDate = table.Column<DateTimeOffset>(type: "DATETIMEOFFSET", nullable: true),
                    ReferenceId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: true),
                    State = table.Column<byte>(type: "TINYINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItem", x => x.TodoItemId);
                    table.ForeignKey(
                        name: "FK_TodoItem_TodoList_TodoListId",
                        column: x => x.TodoListId,
                        principalTable: "TodoList",
                        principalColumn: "TodoListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItem_TodoListId",
                table: "TodoItem",
                column: "TodoListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditEntry");

            migrationBuilder.DropTable(
                name: "TodoItem");

            migrationBuilder.DropTable(
                name: "TodoList");
        }
    }
}
