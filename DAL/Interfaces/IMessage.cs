using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface IMessage
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid TopicId { get; set; }

        public Topic Topic { get; set; }

        public Guid User_Id { get; set; }

    }
}
