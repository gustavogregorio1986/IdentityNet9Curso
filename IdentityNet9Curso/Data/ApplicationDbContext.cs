using IdentityNet9Curso.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityNet9Curso.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // 1. Construtor normal usado pela aplicação web (Program.cs)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // 2. Construtor vazio/padrão que o EF Core usa apenas para gerar as migrations
        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Se as opções não estiverem configuradas (o que acontece no comando dotnet ef), 
            // ele aponta direto para o SQL Server com a sua string de conexão
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}