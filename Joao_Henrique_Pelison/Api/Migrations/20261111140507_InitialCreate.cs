using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TabelaFuncionario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    cpf = table.Column<string>(type: "TEXT", nullable: false),
                    mes = table.Column<int>(type: "INTEGER", nullable: false),
                    ano = table.Column<int>(type: "INTEGER", nullable: false),
                    horasTrabalhadas = table.Column<int>(type: "INTEGER", nullable: false),
                    valorHora = table.Column<int>(type: "INTEGER", nullable: false),
                    SalarioBruto = table.Column<float>(type: "REAL", nullable: false),
                    ImpostoRenda = table.Column<float>(type: "REAL", nullable: false),
                    Inss = table.Column<float>(type: "REAL", nullable: false),
                    Fgts = table.Column<float>(type: "REAL", nullable: false),
                    SalarioLiquido = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TabelaFuncionario", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TabelaFuncionario");
        }
    }
}
