using BPOSolution.Models;
using Microsoft.EntityFrameworkCore;

namespace BPOSolution.Entity
{
    public class DataContext : DbContext
    {
        //DataContext is your database
        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        //The following represent your tables in the database
        //public DbSet<__Model Name__> __LocalObjectName__ { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<BPOClient> BPOClient { get; set; }
        //public DbSet<Category> Category { get; set; }
        //public DbSet<Models.Portfolio> Portfolio { get; set; }


    }
}
