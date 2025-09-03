using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ExperisSeguros.Data.Migrations
{
    public partial class AddClientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "209512fd-2892-42a8-8e0c-c17396deba79", "AQAAAAEAACcQAAAAELAEhAeFwP5SuO8rnh8urNAC/MQRVA8N84FdYi8th5GAIgFdlvKqHvvFPorFkoqBHw==", "d0cd9d80-654a-49f5-afcf-e0151968fda7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de44926a-00f6-4f2a-a586-9dde7b5aa535", "AQAAAAEAACcQAAAAENHCTbrMyQK89Y3hoVOTfm8bOHrJsLFOKDyxRjDo9xMqvqnPC1+op5LhSUoA5HmiiA==", "fcec57f7-c41c-4fc6-b0fc-c1916686c707" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5626a43a-fa5c-4033-80c4-a55ce659f8cd", "AQAAAAEAACcQAAAAEIyh+PrvmruUuhYdLMH5znTSgIx26kJjrcbLQb9HNO5WYqjFxoQgBItLI+SQmxgfqg==", "13187626-af1a-41d9-ab60-aa4c19d4bda2" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ApellidoMaterno", "ApellidoPaterno", "ConcurrencyStamp", "Email", "EmailConfirmed", "FechaNacimiento", "Genero", "LockoutEnabled", "LockoutEnd", "Nombre", "NormalizedEmail", "NormalizedUserName", "PaisId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "c28be9c0-aa65-4af8-bd17-00bd9344e581", 0, "Lozano", "Sánchez", "a8b8cfcb-282d-427e-9c2b-9352a51a31fc", "mariana.sanchez@gmail.com", true, new DateTime(2000, 9, 3, 0, 0, 0, 0, DateTimeKind.Local), 1, false, null, "Mariana", "MARIANA.SANCHEZ@GMAIL.COM", "MARIANA.SANCHEZ@GMAIL.COM", 1, "AQAAAAEAACcQAAAAEI8a540LuTjOhTB8DtMr6tMeuwkSV7XAbCqtWgVpYrTWQd+gbIErGCTqUKdE/Jthdg==", "+5215512345673", true, "473ba297-08a8-4517-9e89-373ddfa284c5", false, "mariana.sanchez@gmail.com" },
                    { "c38be9c0-aa65-4af8-bd17-00bd9344e582", 0, "Morales", "Ramírez", "82fbd178-fdba-4591-abb7-28129c8c71eb", "jorge.ramirez@gmail.com", true, new DateTime(1990, 9, 3, 0, 0, 0, 0, DateTimeKind.Local), 0, false, null, "Jorge", "JORGE.RAMIREZ@GMAIL.COM", "JORGE.RAMIREZ@GMAIL.COM", 1, "AQAAAAEAACcQAAAAEHGJQiBiQmoJxKRw/jBEeWiV4Efe4yB1Y5Yt6vDJxDoKs6WsPa+E9FreErl8QwfpGA==", "+5215512345674", true, "f55e5c6a-9718-4df9-827a-1718855ee99d", false, "jorge.ramirez@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "c28be9c0-aa65-4af8-bd17-00bd9344e581", "c18be9c0-aa65-4af8-bd17-00bd9344e577" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "c38be9c0-aa65-4af8-bd17-00bd9344e582", "c18be9c0-aa65-4af8-bd17-00bd9344e577" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "c28be9c0-aa65-4af8-bd17-00bd9344e581", "c18be9c0-aa65-4af8-bd17-00bd9344e577" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "c38be9c0-aa65-4af8-bd17-00bd9344e582", "c18be9c0-aa65-4af8-bd17-00bd9344e577" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c28be9c0-aa65-4af8-bd17-00bd9344e581");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c38be9c0-aa65-4af8-bd17-00bd9344e582");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-00bd9344e578",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf411354-c8e5-466c-9223-e078ad0112a1", "AQAAAAEAACcQAAAAEKaIfvkj3Ev8xualACxKZ5NpBY6gyzv8irI4rqGjB2JxYT86ZPocbMYUp4I5IH0HOA==", "a04c2a69-9b0d-4d3a-b80a-c017f0072168" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b18be9c0-aa65-4af8-bd17-00bd9344e579",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc18ba8b-20a0-4576-b467-31751cf7aa94", "AQAAAAEAACcQAAAAEB3HY7Ll8AKcU6LbvlEPt/C+sP8q9/sGasbYCjefITZ8MSLJV9yICAwRPX/cRdZO+g==", "d77ceab9-8148-4dac-a365-613cd4fe9238" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c18be9c0-aa65-4af8-bd17-00bd9344e580",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "86d82f78-1f38-4696-a769-8b25dd4fc999", "AQAAAAEAACcQAAAAEGd9YLnr8IOOF/q2TSngVvbZg5ApyvTjUApPr8dMN0ypnoX6xSK7Sk4RfP38JzKx6Q==", "fbab6548-ce4c-40fa-a752-cb0d9c29a82f" });
        }
    }
}
