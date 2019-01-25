using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models
{
    //модель пользователей
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Profile> Profiles { get; set; }

        public User()
        {
            Profiles = new List<Profile>();
        }

        public override string ToString()
        {
            return Name;
        }

    }
}