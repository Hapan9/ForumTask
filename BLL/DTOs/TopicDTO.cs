using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class TopicDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
