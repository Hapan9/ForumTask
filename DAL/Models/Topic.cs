using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Topic
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public virtual IEnumerable<Message> Messages { get; set; }
    }
}