using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IUser
    {
        Guid Id { get; set; }

        string Name { get; set; }

        string Surname { get; set; }

        public string Login { get; set; }

        public Guid Password { get; set; }

        Enums.Roles Role { get; set; }

        IEnumerable<Topic> Topics { get; set; }

        IEnumerable<Message> Messages { get; set; }
    }
}
