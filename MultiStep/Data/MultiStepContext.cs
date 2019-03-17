using Microsoft.EntityFrameworkCore;
using MultiStep.Models;

namespace MultiStep.Data
{
    public class MultiStepContext : DbContext
    {
        public MultiStepContext (DbContextOptions<MultiStepContext> options)
            : base(options)
        {
        }

        public DbSet<Registration> Registration { get; set; }
    }
}
