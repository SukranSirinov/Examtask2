using Examtask2.Models;
using Microsoft.EntityFrameworkCore;

namespace Examtask2.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Comments> comments { get; set; }

    }
}
