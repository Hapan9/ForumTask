using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using DAL.Interfaces;
using DAL.Models;

namespace DAL.Models
{
    public class Topic: ITopic
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [NotMapped]
        public Guid User_Id { get; set; }
    }
}
