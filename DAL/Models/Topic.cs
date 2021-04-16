using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DAL.Interfaces;
using DAL.Models;

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
