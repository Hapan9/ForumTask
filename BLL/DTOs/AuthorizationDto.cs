using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class AuthorizationDto
    {
        [Required] public string Login { get; set; }

        [Required] public string Password { get; set; }
    }
}