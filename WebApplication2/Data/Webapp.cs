using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class Webapp:DbContext 
    {
        public Webapp(DbContextOptions<Webapp> options) : base(options) { }
        public DbSet<Item> Items { get; set; }

    }
}
