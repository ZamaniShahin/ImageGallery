using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ImageGallery.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    public AppDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(""); // todo: take from configurations
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // todo: add fluents
        base.OnModelCreating(modelBuilder);
    }
}

public class DatabaseBuilder(IConfiguration configuration) : IDesignTimeDbContextFactory<AppDbContext>
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new NullReferenceException();
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(_connectionString);
        return new AppDbContext(optionsBuilder.Options);
    }
}