using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class MessageDTO
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public Guid TopicId { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
