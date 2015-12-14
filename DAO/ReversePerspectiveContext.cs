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

        public DbSet<Paragraph> Paragraph { get; set; }
    }
}
