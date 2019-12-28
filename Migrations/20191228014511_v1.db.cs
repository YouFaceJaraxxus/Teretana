using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Teretana.Migrations
{
    public partial class v1db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MembershipTypes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    active = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    price = table.Column<double>(nullable: false),
                    productImage = table.Column<byte[]>(nullable: true),
                    state = table.Column<int>(nullable: false),
                    type = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    date = table.Column<DateTime>(nullable: false),
                    total = table.Column<double>(nullable: false),
                    type = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    active = table.Column<bool>(nullable: false),
                    logged = table.Column<bool>(nullable: false),
                    master = table.Column<bool>(nullable: false),
                    password = table.Column<string>(nullable: true),
                    type = table.Column<bool>(nullable: false),
                    username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    JMB = table.Column<string>(nullable: true),
                    active = table.Column<bool>(nullable: false),
                    birthDate = table.Column<DateTime>(nullable: false),
                    gender = table.Column<bool>(nullable: false),
                    imgBytes = table.Column<byte[]>(nullable: true),
                    joinDate = table.Column<DateTime>(nullable: false),
                    lastName = table.Column<string>(nullable: true),
                    membershipTypeid = table.Column<int>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    untilDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_MembershipTypes_membershipTypeid",
                        column: x => x.membershipTypeid,
                        principalTable: "MembershipTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseItems",
                columns: table => new
                {
                    productId = table.Column<int>(nullable: false),
                    purchaseId = table.Column<int>(nullable: false),
                    count = table.Column<int>(nullable: false),
                    productId1 = table.Column<int>(nullable: true),
                    purchaseId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseItems", x => new { x.productId, x.purchaseId });
                    table.ForeignKey(
                        name: "FK_PurchaseItems_Products_productId1",
                        column: x => x.productId1,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseItems_Purchases_purchaseId1",
                        column: x => x.purchaseId1,
                        principalTable: "Purchases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MembershipFees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    date = table.Column<DateTime>(nullable: false),
                    discount = table.Column<double>(nullable: false),
                    memberId = table.Column<int>(nullable: true),
                    membershipTypeid = table.Column<int>(nullable: true),
                    numberOfMonths = table.Column<int>(nullable: false),
                    price = table.Column<double>(nullable: false),
                    realPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembershipFees_Members_memberId",
                        column: x => x.memberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MembershipFees_MembershipTypes_membershipTypeid",
                        column: x => x.membershipTypeid,
                        principalTable: "MembershipTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    TrainingDate = table.Column<DateTime>(nullable: false),
                    EndingDate = table.Column<DateTime>(nullable: false),
                    MemberId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => new { x.Id, x.TrainingDate });
                    table.ForeignKey(
                        name: "FK_Trainings_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingFees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Autoincrement", true),
                    TrainingDate = table.Column<DateTime>(nullable: true),
                    price = table.Column<double>(nullable: false),
                    trainingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingFees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingFees_Trainings_trainingId_TrainingDate",
                        columns: x => new { x.trainingId, x.TrainingDate },
                        principalTable: "Trainings",
                        principalColumns: new[] { "Id", "TrainingDate" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_membershipTypeid",
                table: "Members",
                column: "membershipTypeid");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipFees_memberId",
                table: "MembershipFees",
                column: "memberId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipFees_membershipTypeid",
                table: "MembershipFees",
                column: "membershipTypeid");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_productId1",
                table: "PurchaseItems",
                column: "productId1");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_purchaseId1",
                table: "PurchaseItems",
                column: "purchaseId1");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_MemberId",
                table: "Trainings",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingFees_trainingId_TrainingDate",
                table: "TrainingFees",
                columns: new[] { "trainingId", "TrainingDate" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MembershipFees");

            migrationBuilder.DropTable(
                name: "PurchaseItems");

            migrationBuilder.DropTable(
                name: "TrainingFees");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "MembershipTypes");
        }
    }
}
