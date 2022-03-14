using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Stefanini.DAO
{
    public class StefaniniContext : DbContext
    {
        public StefaniniContext()
        {
        }

        public StefaniniContext(DbContextOptions<StefaniniContext> options) : base(options) {  }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile($"appsettings.json");

            var config = builder.Build();

            var str = config.GetConnectionString("STRSQL");

            optionsBuilder.UseSqlServer(str);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
