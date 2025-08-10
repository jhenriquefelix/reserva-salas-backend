using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservaSalas.Infraestrutura.Migrations
{
    /// <inheritdoc />
    public partial class SeedLocaisSalas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var local1Id = new Guid("11111111-1111-1111-1111-111111111111");
            var local2Id = new Guid("22222222-2222-2222-2222-222222222222");

            migrationBuilder.InsertData(
                table: "Locais",
                columns: new[] { "Id", "Nome", "RowVersion" },
                values: new object[,]
                {
                    { local1Id, "Matriz",      new byte[] { 0 } },
                    { local2Id, "Filial",      new byte[] { 0 } }
                }
            );

            migrationBuilder.InsertData(
                table: "Salas",
                columns: new[] { "Id", "LocalId", "Nome", "Capacidade", "RowVersion" },
                values: new object[,]
                {
                    { new Guid("aaaa1111-1111-1111-1111-111111111111"), local1Id, "Sala de Reunião 1", 10, new byte[] { 0 } },
                    { new Guid("aaaa2222-2222-2222-2222-222222222222"), local1Id, "Auditório",         50, new byte[] { 0 } },
                    { new Guid("aaaa3333-3333-3333-3333-333333333333"), local1Id, "Sala Pequena",       5, new byte[] { 0 } },

                    { new Guid("bbbb1111-1111-1111-1111-111111111111"), local2Id, "Sala de Treinamento", 20, new byte[] { 0 } },
                    { new Guid("bbbb2222-2222-2222-2222-222222222222"), local2Id, "Sala de Reunião 2",    12, new byte[] { 0 } },
                    { new Guid("bbbb3333-3333-3333-3333-333333333333"), local2Id, "Sala VIP",              8, new byte[] { 0 } }
                }
            );
        }



        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // GUIDs iguais aos usados no Up
            var local1Id = new Guid("11111111-1111-1111-1111-111111111111");
            var local2Id = new Guid("22222222-2222-2222-2222-222222222222");

            // Salas do Local 1
            migrationBuilder.DeleteData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: new Guid("aaaa1111-1111-1111-1111-111111111111")
            );
            migrationBuilder.DeleteData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: new Guid("aaaa2222-2222-2222-2222-222222222222")
            );
            migrationBuilder.DeleteData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: new Guid("aaaa3333-3333-3333-3333-333333333333")
            );

            // Salas do Local 2
            migrationBuilder.DeleteData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: new Guid("bbbb1111-1111-1111-1111-111111111111")
            );
            migrationBuilder.DeleteData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: new Guid("bbbb2222-2222-2222-2222-222222222222")
            );
            migrationBuilder.DeleteData(
                table: "Salas",
                keyColumn: "Id",
                keyValue: new Guid("bbbb3333-3333-3333-3333-333333333333")
            );

            // Por fim, remover os Locais
            migrationBuilder.DeleteData(
                table: "Locais",
                keyColumn: "Id",
                keyValue: local1Id
            );
            migrationBuilder.DeleteData(
                table: "Locais",
                keyColumn: "Id",
                keyValue: local2Id
            );
        }

    }
}
