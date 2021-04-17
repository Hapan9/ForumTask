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

namespace BLL
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
            if (userDto.Login.Length < 4 || userDto.Password.Length < 4 || userDto.Name.Length < 4)
                throw new ArgumentException("User if undefined");
            if (userDto.Surname != null && userDto.Surname.Length < 4)
                throw new ArgumentException("User if undefined");
            if ((await _unitOfWork.Users.GetAll()).Count(u => u.Login == userDto.Login) > 0)
                throw new InvalidCastException("User with such login already exists");

            var newUser = _mapper.Map<User>(userDto);

            await _unitOfWork.Users.Create(newUser);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _unitOfWork.Users.GetAll();
        }

        public async Task<User> GetUser(Guid id)
        {
            if (await _unitOfWork.Users.Get(id) == null)
                throw new ArgumentException("User if undefined");

            return await _unitOfWork.Users.Get(id);
        }

        public async Task UpdateUser(Guid id, UserDto userDto)
        {
            if (await _unitOfWork.Users.Get(id) == null)
                throw new ArgumentException("User if undefined");
            if (userDto.Login.Length < 4 || userDto.Password.Length < 4 || userDto.Name.Length < 4)
                throw new ArgumentException("Invalid input");
            if (userDto.Surname != null && userDto.Surname.Length < 4)
                throw new ArgumentException("Invalid input");
            if ((await _unitOfWork.Users.GetAll()).Count(u => u.Login == userDto.Login && u.Id != id) > 0)
                throw new InvalidCastException("User with such login already exists");

            var updatedUser = _mapper.Map<User>(userDto);

            updatedUser.Id = id;

            await _unitOfWork.Users.Update(updatedUser);
        }

        public async Task DeleteUser(Guid id)
        {
            if (await _unitOfWork.Users.Get(id) == null)
                throw new ArgumentException("User if undefined");

            await _unitOfWork.Users.Delete(id);
        }

        public async Task<User> CheckUserForm(AuthorizationDto authorizationDto)
        {
            if ((await _unitOfWork.Users.GetAll()).Count(u => u.Login == authorizationDto.Login) == 0)
                throw new ArgumentException("User if undefined");

            if ((await _unitOfWork.Users.GetAll()).First(u => u.Login == authorizationDto.Login).Password ==
                Hashing.GetHashString(authorizationDto.Password))
                return (await _unitOfWork.Users.GetAll()).First(u => u.Login == authorizationDto.Login);

            return null;
        }

        public async Task<IEnumerable<Topic>> GetTopics(Guid id)
        {
            if (await _unitOfWork.Users.Get(id) == null)
                throw new ArgumentException("User if undefined");

            var topicList = (await _unitOfWork.Topics.GetAll()).Where(t => t.UserId == id).ToList();

            return topicList;
        }

        public async Task<IEnumerable<Message>> GetMessages(Guid id)
        {
            if ((await _unitOfWork.Users.GetAll()).Count(u => u.Id == id) == 0)
                throw new ArgumentException("User if undefined");

            var messageList = (await _unitOfWork.Messages.GetAll()).Where(m => m.UserId == id).ToList();

            return messageList;
        }
    }
}