using System.ComponentModel.DataAnnotations;

namespace PL.Models
{
    public class RegistrationModel
    {
        [Required] public string Name { get; set; }

        public string Surname { get; set; }

        [Required] public string Login { get; set; }

        [Required] public string Password { get; set; }
    }
}