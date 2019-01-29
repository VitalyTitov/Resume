using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //для связи с связующей таблицей
        public int? UserId { get; set; }
        public virtual User User { get; set; }

        //блок для ролей
        public virtual ICollection<Role> Roles { get; set; }
 
        //блок для юзеров
        public virtual ICollection<User> Users { get; set; }

        public Profile()
        {
            Roles = new List<Role>();
            Users = new List<User>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}