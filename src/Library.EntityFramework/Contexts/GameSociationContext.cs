using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Library.EntityFramework.Contexts
{
    public class GameSociationContext : DbContext
    {
        public GameSociationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}