using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class AuthorizationDto
    {
        [Required] [MinLength(5)] public string Login { get; set; }

        [Required] [MinLength(5)] public string Password { get; set; }
    }
}