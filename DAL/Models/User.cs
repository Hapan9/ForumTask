using System;
using System.Collections.Generic;
using DAL.Enums;

namespace DAL.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Login { get; set; }

        public Guid Password { get; set; }

        public Roles Role { get; set; }

        public virtual IEnumerable<Topic> Topics { get; set; }

        public virtual IEnumerable<Message> Messages { get; set; }
    }
}