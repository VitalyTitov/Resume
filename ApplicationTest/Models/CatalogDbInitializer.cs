using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace Test.Models
{//тестовый
    public class CatalogDbInitializer : DropCreateDatabaseAlways<CatalogContext>
    {
        protected override void Seed(CatalogContext db)
        {
            Role r1 = new Role { Id = 1, Name = "Reader", Description = "Разрешение на чтение записей" };
            Role r2 = new Role { Id = 2, Name = "Writer", Description = "Разрешение на создание записей" };
            db.Roles.Add(r1);
            db.Roles.Add(r2);

            Profile p1 = new Profile { Id = 1, Name = "Default", Roles = new List<Role>() { r1} };
            Profile p2 = new Profile { Id = 2, Name = "Admin", Roles = new List<Role>() { r1,r2 } };
            db.Profiles.Add(p1);
            db.Profiles.Add(p2);

            User u1 = new User {Id=1, Name = "Виталий", Login = "Viten", Profiles = new List<Profile>() { p1,p2} };
            db.Users.Add(u1);

            base.Seed(db);
        }
    }
}