using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PcBuilderBackend.Persistence.Contexts;

namespace PcBuilderBackend.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(
                "Host=127.0.0.1;Port=5432;Database=pcbuilderdb;Username=pcbuilder;Password=PcBuilder2024!");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
