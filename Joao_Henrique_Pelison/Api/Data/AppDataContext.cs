using Microsoft.EntityFrameworkCore;
using Api.Modelo;
namespace Api.Data;

public class AppDbContext : DbContext
{
    public DbSet<Funcionarios> TabelaFuncionario {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=TabelaFuncionario.db");
    }
}