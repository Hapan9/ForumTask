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

        Task<IEnumerable<UserDto>> GetUsers();

        Task CreateUser(UserDto userDto);

        Task UpdateUser(Guid id, UserDto userDto);

        Task DeleteUser(Guid id);

        Task<UserDto> CheckUserForm(AuthorizationDto authorizationDto);

        Task<IEnumerable<TopicDto>> GetTopics(Guid id);

        Task<IEnumerable<MessageDto>> GetMessages(Guid id);
    }
}