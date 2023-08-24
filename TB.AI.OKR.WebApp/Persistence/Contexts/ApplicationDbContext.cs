using Microsoft.EntityFrameworkCore;
using TB.AI.OKR.WebApp.Persistence.Entities;

namespace TB.AI.OKR.WebApp.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        #region DbSets
        public DbSet<KeyResult> KeyResults { get; set; }
        public DbSet<Objective> Objectives { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SampleOkr> SampleOkrs { get; set; }
        #endregion

        public string DbPath { get; }


        public ApplicationDbContext()
        {
            DbPath = "./Persistence/Database/sample-okrs.db";
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
