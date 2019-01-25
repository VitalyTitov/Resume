using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //public int? ProfileId { get; set; }
        //public virtual Profile Profile { get; set; }
        public virtual ICollection<Profile> Profiles { get; set; }

        public Role()
        {
            Profiles = new List<Profile>();
        }
    }
}