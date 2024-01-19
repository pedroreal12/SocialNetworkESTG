using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialNetworkMovies.Areas.Identity.Data;

namespace SocialNetworkMovies.Data;

public class SocialNetworkMoviesContext : IdentityDbContext<SocialNetworkMoviesUser>
{
    public SocialNetworkMoviesContext()
    {
    }

    public SocialNetworkMoviesContext(DbContextOptions<SocialNetworkMoviesContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Use your preferred database provider and connection string here
            optionsBuilder.UseSqlServer("Server=localhost, 1433;Database=snidentitydb;User Id=SA;Password=A&VeryComplex123Password;TrustServerCertificate=True;MultipleActiveResultSets=true");
        }
    }
}
