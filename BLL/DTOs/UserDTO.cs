using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DAL.Enums;

namespace BLL.DTOs
{
    public class UserDTO
    {
        [Required]
        public string Name { get; set; }

        public string Surname { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public Roles Role { get; set; }

    }
}
