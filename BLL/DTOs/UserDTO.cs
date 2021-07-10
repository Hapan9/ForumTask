using System.ComponentModel.DataAnnotations;
using DAL.Enums;

namespace BLL.DTOs
{
    public class UserDto
    {
        [Required] [MinLength(5)] public string Name { get; set; }

        [MinLength(5)] public string Surname { get; set; }

        [Required] [MinLength(5)] public string Login { get; set; }

        [Required] [MinLength(5)] public string Password { get; set; }

        [Required] public Roles Role { get; set; }
    }
}