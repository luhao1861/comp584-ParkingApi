using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParkingModel;

namespace ParkingModel;

public partial class ParkingContext : IdentityDbContext<ParkingUser>
{
    public ParkingContext()
    {
    }

    public ParkingContext(DbContextOptions<ParkingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ParkingLot> ParkingLots { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            //.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
            ;
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
