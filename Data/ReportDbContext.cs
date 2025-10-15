using Microsoft.EntityFrameworkCore;
using PROG7312_POEPART2.Models;

namespace PROG7312_POEPART2.Data
{
    public class ReportDbContext : DbContext
    {
        public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options) { }

        public DbSet<Issue> Issues { get; set; }
    }
}
