using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Test.Models
{
    public class CatalogContext: DbContext
    {
        public CatalogContext()
        : base("Catalogdb")
        { }

        public DbSet<User> Users{ get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Role> Roles{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //создание связующей таблицы User и Profile
            modelBuilder.Entity<Profile>().HasMany(c => c.Users)
                .WithMany(s => s.Profiles)
                .Map(t => t.MapLeftKey("ProfileId")
                .MapRightKey("UserId")               
                .ToTable("ProfileUser"));
            //создание связующей таблицы Role и Profile
            modelBuilder.Entity<Role>().HasMany(c => c.Profiles)
                .WithMany(s => s.Roles)
                .Map(t => t.MapLeftKey("RoleId")
                .MapRightKey("ProfileId")
                .ToTable("RoleProfile"));
        }       
    }
}