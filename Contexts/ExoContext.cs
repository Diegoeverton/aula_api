using Exo.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace Exo.WebApi.Contexts
{
    public class ExoContext : DbContext
    {
        public ExoContext()
        {

        }
        public ExoContext (DbContextOptions <ExoContext> options) : base (options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    if (!optionsBuilder.IsConfigured)
    {
        // Essa string de conexao depende da sua maquina
        //optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ExoApi;Trusted_Connection=True;");
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;" + "Database=ExoApi;Trusted_Connection=True");

        // Example 1 of connection string:
        // User ID=sa;Password=admin;Server=localhost;Database=ExoApi;Trusted_Connection=False;

        // Example 2 of connection string:
        // Server=localhost\\SQLEXPRESS;Database=ExoApi;Trusted_Connection=True;
    }
}

        public DbSet<Projeto> Projetos { get; set;}
    }
}