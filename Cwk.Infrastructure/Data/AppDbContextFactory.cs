using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cwk.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer( "Server=localhost,1433; Database=CWKDB; User Id=jmartinez; Password=Password123!; TrustServerCertificate=True;");
        return new AppDbContext(optionsBuilder.Options);
    }
}

