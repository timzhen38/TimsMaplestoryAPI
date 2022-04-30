using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TimsMaplestoryAPI.Models;

namespace TimsMaplestoryAPI.Models
{
    public class MaplestoryAPIDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public MaplestoryAPIDBContext(DbContextOptions<MaplestoryAPIDBContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("maplestoryplayerdata");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Class> Classes { get; set; } = null!;
    }
}