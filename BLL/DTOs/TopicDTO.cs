using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class TopicDto
    {
        [Required] public string Name { get; set; }

        [Required] public Guid UserId { get; set; }
    }
}