using meta.challengeWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace meta.challengeWebApi.Context
{
    public class SqlContext : DbContext
    {
        public DbSet<Contato> Contatos { get; set; }

        public SqlContext(DbContextOptions<SqlContext> options)
            :base(options) 
        {
            this.Database.EnsureCreated();
        }
    }
}
