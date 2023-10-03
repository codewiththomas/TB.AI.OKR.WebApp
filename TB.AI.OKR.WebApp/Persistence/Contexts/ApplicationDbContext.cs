using Microsoft.EntityFrameworkCore;
using TB.AI.OKR.WebApp.Persistence.Entities;
using TB.AI.OKR.WebApp.Persistence.Seeds;

namespace TB.AI.OKR.WebApp.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        #region DbSets
        public DbSet<Label<OkrSet>> OkrSetLabels { get; set; }
        public DbSet<Label<OkrSetElement>> OkrSetElementLabels { get; set; }

        public DbSet<OkrSet> OkrSets { get; set; }
        public DbSet<OkrSetElement> OkrSetElements { get; set; }

        public DbSet<Review> Reviews { get; set; }        
        public DbSet<OkrRule> OkrRules { get; set; }

        public DbSet<ReferenceSource> ReferenceSources { get; set; }
        #endregion

        public string DbPath { get; }


        public ApplicationDbContext()
        {
            //DbPath = "../../../Persistence/Database/sample-okrs.db";
            DbPath = "D:/FOM/Repos/TB.AI.OKR.WebApp/TB.AI.OKR.WebApp/Persistence/Database/sample-okrs.db";
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedReferenceSources();
            modelBuilder.SeedRules();

            base.OnModelCreating(modelBuilder);
        }
    }
}
