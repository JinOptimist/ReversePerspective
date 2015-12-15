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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Opus>().HasMany(opus => opus.Scenes).WithRequired(scene => scene.Opus);
            modelBuilder.Entity<Opus>().HasMany(opus => opus.Heroes).WithRequired(hero => hero.Opus);
            modelBuilder.Entity<Scene>().HasMany(scene => scene.Phrases).WithRequired(phrase => phrase.Scene);
            modelBuilder.Entity<Hero>().HasMany(hero => hero.HeroInfo).WithRequired(heroAdditionalInfo => heroAdditionalInfo.Hero);
            
            //modelBuilder.Entity<Course>()
            //    .HasMany(c => c.Instructors).WithMany(i => i.Courses)
            //    .Map(t => t.MapLeftKey("CourseID")
            //        .MapRightKey("InstructorID")
            //        .ToTable("CourseInstructor"));
            //modelBuilder.Entity<Department>().MapToStoredProcedures();
        }
    }
}
