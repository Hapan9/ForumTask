using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface IUserServise
    {

        Task<User> GetUser(Guid id);

        Task<IEnumerable<User>> GetUsers();

        Task CreateUser(UserDTO userDTO);

        Task UpadteUser(Guid id, UserDTO userDTO);

        Task DeleteUser(Guid id);

        Task<User> CheckUserForm(AutorizationDTO autorizationDTO);

        Task<IEnumerable<Topic>> GetTopics(Guid id);

        Task<IEnumerable<Message>> GetMessages(Guid id);
    }
}