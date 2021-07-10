using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTOs;
using BLL.Interfaces;
using BLL.Mappers;
using DAL.Interfaces;
using DAL.Models;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = AutoMapperProfile.InitializeAutoMapper().CreateMapper();
        }

        public async Task CreateUser(UserDto userDto)
        {
            if ((await _unitOfWork.Users.GetAll()).Count(u => u.Login == userDto.Login) > 0)
                throw new ArgumentException("User with such login already exists");

            var newUser = _mapper.Map<User>(userDto);

            await _unitOfWork.Users.Create(newUser);

            await _unitOfWork.Save();
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            return _mapper.Map<IEnumerable<UserDto>>(await _unitOfWork.Users.GetAll());
        }

        public async Task<User> GetUser(Guid id)
        {
            if (await _unitOfWork.Users.Get(id) == null)
                throw new ArgumentException("User is undefined");

            return await _unitOfWork.Users.Get(id);
        }

        public async Task UpdateUser(Guid id, UserDto userDto)
        {
            if (await _unitOfWork.Users.Get(id) == null)
                throw new ArgumentException("User is undefined");
            if ((await _unitOfWork.Users.GetAll()).Count(u => u.Login == userDto.Login && u.Id != id) > 0)
                throw new ArgumentException("User with such login already exists");

            var updatedUser = _mapper.Map<User>(userDto);

            updatedUser.Id = id;

            await _unitOfWork.Users.Update(updatedUser);

            await _unitOfWork.Save();
        }

        public async Task DeleteUser(Guid id)
        {
            if (await _unitOfWork.Users.Get(id) == null)
                throw new ArgumentException("User is undefined");

            await _unitOfWork.Users.Delete(id);

            await _unitOfWork.Save();
        }

        public async Task<UserDto> CheckUserForm(AuthorizationDto authorizationDto)
        {
            if ((await _unitOfWork.Users.GetAll()).Count(u => u.Login == authorizationDto.Login) == 0)
                throw new ArgumentException("User is undefined");

            if ((await _unitOfWork.Users.GetAll()).First(u => u.Login == authorizationDto.Login).Password ==
                Hashing.GetHashString(authorizationDto.Password))
                return _mapper.Map<UserDto>(
                    (await _unitOfWork.Users.GetAll()).First(u => u.Login == authorizationDto.Login));

            return null;
        }

        public async Task<IEnumerable<TopicDto>> GetTopics(Guid id)
        {
            if (await _unitOfWork.Users.Get(id) == null)
                throw new ArgumentException("User is undefined");

            var topicList = (await _unitOfWork.Topics.GetAll()).Where(t => t.UserId == id).ToList();

            return _mapper.Map<IEnumerable<TopicDto>>(topicList);
        }

        public async Task<IEnumerable<MessageDto>> GetMessages(Guid id)
        {
            if (await _unitOfWork.Users.Get(id) == null)
                throw new ArgumentException("User is undefined");

            var messageList = (await _unitOfWork.Messages.GetAll()).Where(m => m.UserId == id).ToList();

            return _mapper.Map<IEnumerable<MessageDto>>(messageList);
        }
    }
}