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
                    Abstract = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
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
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ESignature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resolved = table.Column<bool>(type: "bit", nullable: true),
                    Accepted = table.Column<bool>(type: "bit", nullable: true),
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
                    { 0, "Lets create a new campus in surrey", new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1170), "221fedc9-3ad4-492e-bfc0-20f198923a24", "Accepted" },
                    { 1, "Lets rebuild the Tech building", new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1210), "221fedc9-3ad4-492e-bfc0-20f198923a24", "Rejected" },
                    { 2, "Lets create more bathooms", new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1210), "37c1ba03-d67c-437e-ac19-2b38b123c55a", "Draft" },
                    { 3, "Replace the mascot with a more appropiate candidate", new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1210), "5559d343-5062-4cd1-b0ae-25301e70a10d", "In Progress" }
                });

            migrationBuilder.InsertData(
                table: "FeedbackRequest",
                columns: new[] { "FeedbackRequestId", "Accepted", "CreationDate", "Description", "ESignature", "OwnerUserID", "ResolutionId", "Resolved" },
                values: new object[,]
                {
                    { 0, true, new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1270), "sure", "jane", "37c1ba03-d67c-437e-ac19-2b38b123c55a", 0, true },
                    { 2, true, new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1270), "Why not", "bob", "d34e5684-030b-4bf1-ba0b-51c424468294", 0, true },
                    { 3, true, new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1270), "Sounds good", "shawn", "c5955b95-5492-4c7b-a3cb-c749c85e3a16", 0, true },
                    { 4, true, new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1280), "i like this", "emily", "5559d343-5062-4cd1-b0ae-25301e70a10d", 0, true },
                    { 5, false, new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1280), "This Resolution can be improved", "jane", "37c1ba03-d67c-437e-ac19-2b38b123c55a", 1, true },
                    { 7, false, new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1280), "This won't work", "bob", "d34e5684-030b-4bf1-ba0b-51c424468294", 1, true },
                    { 8, false, new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1290), "I don't like this idea", "shawn", "c5955b95-5492-4c7b-a3cb-c749c85e3a16", 1, true },
                    { 9, false, new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1290), "I don't think we should go through with this", "emily", "5559d343-5062-4cd1-b0ae-25301e70a10d", 1, true },
                    { 11, false, new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1290), "", "", "221fedc9-3ad4-492e-bfc0-20f198923a24", 2, false },
                    { 12, false, new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1290), "", "", "d34e5684-030b-4bf1-ba0b-51c424468294", 2, false },
                    { 13, false, new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1300), "", "", "c5955b95-5492-4c7b-a3cb-c749c85e3a16", 2, false },
                    { 14, false, new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1300), "", "", "5559d343-5062-4cd1-b0ae-25301e70a10d", 2, false },
                    { 15, false, new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1300), "", "", "37c1ba03-d67c-437e-ac19-2b38b123c55a", 3, false },
                    { 16, false, new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1300), "", "", "221fedc9-3ad4-492e-bfc0-20f198923a24", 3, false },
                    { 17, false, new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1300), "", "", "d34e5684-030b-4bf1-ba0b-51c424468294", 3, true },
                    { 18, false, new DateTime(2022, 11, 19, 21, 0, 17, 421, DateTimeKind.Local).AddTicks(1310), "", "", "c5955b95-5492-4c7b-a3cb-c749c85e3a16", 3, false }
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
