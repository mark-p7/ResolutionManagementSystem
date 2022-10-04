using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResolutionManagement.Data.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resolution",
                columns: table => new
                {
                    ResolutionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Abstract = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerUserID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resolution", x => x.ResolutionId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackRequest",
                columns: table => new
                {
                    FeedbackRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OwnerUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResolutionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackRequest", x => x.FeedbackRequestId);
                    table.ForeignKey(
                        name: "FK_FeedbackRequest_Resolution_ResolutionId",
                        column: x => x.ResolutionId,
                        principalTable: "Resolution",
                        principalColumn: "ResolutionId");
                });

            migrationBuilder.InsertData(
                table: "Resolution",
                columns: new[] { "ResolutionId", "Abstract", "CreationDate", "OwnerUserID", "Status" },
                values: new object[,]
                {
                    { 0, "", new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(4876), "221fedc9-3ad4-492e-bfc0-20f198923a24", "in progress" },
                    { 1, "", new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(4920), "221fedc9-3ad4-492e-bfc0-20f198923a24", "in progress" },
                    { 2, "", new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(4950), "37c1ba03-d67c-437e-ac19-2b38b123c55a", "in progress" },
                    { 3, "", new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(4953), "5559d343-5062-4cd1-b0ae-25301e70a10d", "in progress" }
                });

            migrationBuilder.InsertData(
                table: "FeedbackRequest",
                columns: new[] { "FeedbackRequestId", "CreationDate", "OwnerUserID", "ResolutionId" },
                values: new object[,]
                {
                    { 0, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(4975), "37c1ba03-d67c-437e-ac19-2b38b123c55a", 0 },
                    { 1, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(4980), "221fedc9-3ad4-492e-bfc0-20f198923a24", 0 },
                    { 2, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(4983), "d34e5684-030b-4bf1-ba0b-51c424468294", 0 },
                    { 3, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(4985), "c5955b95-5492-4c7b-a3cb-c749c85e3a16", 0 },
                    { 4, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(4988), "5559d343-5062-4cd1-b0ae-25301e70a10d", 0 },
                    { 5, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(4992), "37c1ba03-d67c-437e-ac19-2b38b123c55a", 1 },
                    { 6, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(4994), "221fedc9-3ad4-492e-bfc0-20f198923a24", 1 },
                    { 7, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(4996), "d34e5684-030b-4bf1-ba0b-51c424468294", 1 },
                    { 8, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(4999), "c5955b95-5492-4c7b-a3cb-c749c85e3a16", 1 },
                    { 9, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(5002), "5559d343-5062-4cd1-b0ae-25301e70a10d", 1 },
                    { 10, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(5004), "37c1ba03-d67c-437e-ac19-2b38b123c55a", 2 },
                    { 11, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(5007), "221fedc9-3ad4-492e-bfc0-20f198923a24", 2 },
                    { 12, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(5009), "d34e5684-030b-4bf1-ba0b-51c424468294", 2 },
                    { 13, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(5011), "c5955b95-5492-4c7b-a3cb-c749c85e3a16", 2 },
                    { 14, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(5014), "5559d343-5062-4cd1-b0ae-25301e70a10d", 2 },
                    { 15, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(5016), "37c1ba03-d67c-437e-ac19-2b38b123c55a", 3 },
                    { 16, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(5019), "221fedc9-3ad4-492e-bfc0-20f198923a24", 3 },
                    { 17, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(5022), "d34e5684-030b-4bf1-ba0b-51c424468294", 3 },
                    { 18, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(5024), "c5955b95-5492-4c7b-a3cb-c749c85e3a16", 3 },
                    { 19, new DateTime(2022, 10, 3, 22, 0, 42, 684, DateTimeKind.Local).AddTicks(5027), "5559d343-5062-4cd1-b0ae-25301e70a10d", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackRequest_ResolutionId",
                table: "FeedbackRequest",
                column: "ResolutionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FeedbackRequest");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Resolution");
        }
    }
}
