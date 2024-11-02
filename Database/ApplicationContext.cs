using Microsoft.EntityFrameworkCore;
using web_rest_hudz_kp21.Models;
public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
    : base(options) { }
    public DbSet<Bicycle> Bicycles { get; set; }
    public DbSet<BikePart> BikeParts { get; set; }
}