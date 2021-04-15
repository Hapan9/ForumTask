using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL.Interfaces
{
    public interface ITopic
    {
        Guid Id { get; set; }

        string Name { get; set; }

        public Guid User_Id { get; set; }
    }
}
