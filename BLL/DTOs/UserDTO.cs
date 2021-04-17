﻿using System.ComponentModel.DataAnnotations;
using DAL.Enums;

namespace BLL.DTOs
{
    public class UserDto
    {
        [Required] public string Name { get; set; }

        public string Surname { get; set; }

        [Required] public string Login { get; set; }

        [Required] public string Password { get; set; }

        [Required] public Roles Role { get; set; }
    }
}