using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class TopicDto
    {
        [Required] [MinLength(1)] public string Name { get; set; }

        [Required] public Guid UserId { get; set; }
    }
}