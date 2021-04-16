using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class AutorizationDTO
    {

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
