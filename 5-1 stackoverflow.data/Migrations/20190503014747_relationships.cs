using Microsoft.EntityFrameworkCore.Migrations;

namespace _5_1_stackoverflow.data.Migrations
{
    public partial class relationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTags_Questions_TagId",
                table: "QuestionTags");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTags_Tags_TagId1",
                table: "QuestionTags");

            migrationBuilder.DropIndex(
                name: "IX_QuestionTags_TagId1",
                table: "QuestionTags");

            migrationBuilder.DropColumn(
                name: "TagId1",
                table: "QuestionTags");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTags_Questions_QuestionId",
                table: "QuestionTags",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTags_Tags_TagId",
                table: "QuestionTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTags_Questions_QuestionId",
                table: "QuestionTags");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionTags_Tags_TagId",
                table: "QuestionTags");

            migrationBuilder.AddColumn<int>(
                name: "TagId1",
                table: "QuestionTags",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTags_TagId1",
                table: "QuestionTags",
                column: "TagId1");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTags_Questions_TagId",
                table: "QuestionTags",
                column: "TagId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionTags_Tags_TagId1",
                table: "QuestionTags",
                column: "TagId1",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
