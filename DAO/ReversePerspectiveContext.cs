using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using DAO.Model;

namespace DAO
{
    public class ReversePerspectiveContext : DbContext
    {
        public ReversePerspectiveContext() : base("name=ReversePerspectiveContext")
        {
            //Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Opus> Opus { get; set; }

        public DbSet<Scene> Scene { get; set; }

        public DbSet<Phrase> Phrase { get; set; }

        public DbSet<Hero> Hero { get; set; }

        public DbSet<HeroAdditionalInfo> HeroAdditionalInfo { get; set; }
    }
}
