using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DAL.Interfaces;

namespace DAL.Models
{
    public class Message:IMessage
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid TopicId { get; set; }

        public Topic Topic { get; set; }

        [NotMapped]
        public Guid User_Id { get; set; }
    }
}
