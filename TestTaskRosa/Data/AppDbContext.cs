using Microsoft.EntityFrameworkCore;
using TestTaskRosa.Models;

namespace TestTaskRosa.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Request> Requests { get; set; }
    }
}
