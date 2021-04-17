using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class MessageDto
    {
        [Required] public string Text { get; set; }

        [Required] public Guid TopicId { get; set; }

        [Required] public Guid UserId { get; set; }
    }
}