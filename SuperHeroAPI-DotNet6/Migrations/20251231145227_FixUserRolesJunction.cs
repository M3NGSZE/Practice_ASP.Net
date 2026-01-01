using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHeroAPI_DotNet6.Migrations
{
    /// <inheritdoc />
    public partial class FixUserRolesJunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_roles_RolesRoleId",
                table: "user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_users_UsersUserId",
                table: "user_roles");

            migrationBuilder.DropIndex(
                name: "IX_user_roles_RolesRoleId",
                table: "user_roles");

            migrationBuilder.DropIndex(
                name: "IX_user_roles_UsersUserId",
                table: "user_roles");

            migrationBuilder.DropColumn(
                name: "RolesRoleId",
                table: "user_roles");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "user_roles");

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "created_at",
                value: new DateTimeOffset(new DateTime(2025, 12, 31, 14, 52, 26, 894, DateTimeKind.Unspecified).AddTicks(9805), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "created_at",
                value: new DateTimeOffset(new DateTime(2025, 12, 31, 14, 52, 26, 894, DateTimeKind.Unspecified).AddTicks(9810), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "created_at",
                value: new DateTimeOffset(new DateTime(2025, 12, 31, 14, 52, 26, 894, DateTimeKind.Unspecified).AddTicks(9812), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_user_roles_roles_role_id",
                table: "user_roles",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_roles_users_user_id",
                table: "user_roles",
                column: "user_id",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_roles_role_id",
                table: "user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_users_user_id",
                table: "user_roles");

            migrationBuilder.AddColumn<Guid>(
                name: "RolesRoleId",
                table: "user_roles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UsersUserId",
                table: "user_roles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "created_at",
                value: new DateTimeOffset(new DateTime(2025, 12, 29, 13, 1, 9, 448, DateTimeKind.Unspecified).AddTicks(2490), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "created_at",
                value: new DateTimeOffset(new DateTime(2025, 12, 29, 13, 1, 9, 448, DateTimeKind.Unspecified).AddTicks(2494), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "roles",
                keyColumn: "role_id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "created_at",
                value: new DateTimeOffset(new DateTime(2025, 12, 29, 13, 1, 9, 448, DateTimeKind.Unspecified).AddTicks(2497), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_RolesRoleId",
                table: "user_roles",
                column: "RolesRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_UsersUserId",
                table: "user_roles",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_user_roles_roles_RolesRoleId",
                table: "user_roles",
                column: "RolesRoleId",
                principalTable: "roles",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_roles_users_UsersUserId",
                table: "user_roles",
                column: "UsersUserId",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
