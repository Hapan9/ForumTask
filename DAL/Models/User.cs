using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces;
using DAL.Enums;
using DAL.Models;

namespace DAL.Models
{
    public class User : IUser {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Login { get; set; }

        public Guid Password { get; set; }

        public Roles Role { get; set; }

        public IEnumerable<Topic> Topics { get; set; }

        public IEnumerable<Message> Messages { get; set; }
    }
}
