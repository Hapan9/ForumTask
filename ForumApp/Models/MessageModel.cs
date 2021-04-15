using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PL.Models
{
    public class MessageModel
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public Guid TopicId { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
