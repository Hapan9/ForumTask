using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTOs;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUser(Guid id);

        Task<IEnumerable<User>> GetUsers();

        Task CreateUser(UserDto userDto);

        Task UpdateUser(Guid id, UserDto userDto);

        Task DeleteUser(Guid id);

        Task<User> CheckUserForm(AuthorizationDto authorizationDto);

        Task<IEnumerable<Topic>> GetTopics(Guid id);

        Task<IEnumerable<Message>> GetMessages(Guid id);
    }
}