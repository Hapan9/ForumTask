using System;
using System.Collections.Generic;
using System.Text.Json;
using DAL.Models;
using DAL;
using DAL.Enums;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.DTOs;

namespace BLL
{
    public class WorkWithUser : IWorkWithUser
    {
        UnitOfWork _unitOfWork;

        public WorkWithUser(Db db)
        {
            _unitOfWork = new UnitOfWork(db);
        }

        public async Task CreateUser(UserDTO userDTO)
        {
            if (userDTO.Login.Length < 4 || userDTO.Password.Length < 4 || userDTO.Name.Length < 4)
                throw new ArgumentException();
            else if (userDTO.Surname != null && userDTO.Surname.Length < 4)
                throw new ArgumentException();
            else if ((await _unitOfWork.Users.GetAll()).Count(u => u.Login == userDTO.Login) > 0)
                throw new InvalidCastException();

            var newUser = new User()
            {
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                Login = userDTO.Login,
                Password = Hashing.GetHashString(userDTO.Password),
                Role = userDTO.Role
            };


            await _unitOfWork.Users.Create(newUser);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _unitOfWork.Users.GetAll();
        }

        public async Task<User> GetUser(Guid id)
        {
            if(_unitOfWork.Users.Get(id) == null)
                throw new ArgumentException();

            return await _unitOfWork.Users.Get(id);
        }

        public async Task UpadteUser(Guid id, UserDTO userDTO)
        {
            if (_unitOfWork.Users.Get(id) == null)
                throw new ArgumentException();
            else if (userDTO.Login.Length < 4 || userDTO.Password.Length < 4 || userDTO.Name.Length < 4)
                throw new ArgumentException();
            else if (userDTO.Surname != null && userDTO.Surname.Length < 4)
                throw new ArgumentException();
            else if ((await _unitOfWork.Users.GetAll()).Count(u => u.Login == userDTO.Login) > 0)
                throw new InvalidCastException();

            var updatedUser = new User()
            {
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                Login = userDTO.Login,
                Password = Hashing.GetHashString(userDTO.Password),
                Role = userDTO.Role,
                Id = id
            };

            await _unitOfWork.Users.Update(updatedUser);
                    
        }

        public async Task DeleteUser(Guid id)
        {
            if (_unitOfWork.Users.Get(id) == null)
                throw new ArgumentException();

            await _unitOfWork.Users.Delete(id);
        }

        public async Task<bool> CheckUserForm(string userLogin, string userPassword)
        {
            if((await _unitOfWork.Users.GetAll()).Count(u => u.Login == userLogin) == 0)
                throw new ArgumentException();

                if ((await _unitOfWork.Users.GetAll()).First(u => u.Login == userLogin).Password != Hashing.GetHashString(userPassword))
                    return false;
                else 
                    return true;
        }

        public async Task<IEnumerable<Topic>> GetTopics(Guid id)
        {
            if (_unitOfWork.Users.Get(id) == null)
                throw new ArgumentException();

            return (await _unitOfWork.Topics.GetAll()).Where(t => t.UserId == id).ToList();
        }

        public async Task<IEnumerable<Message>> GetMessages(Guid id)
        {
            if ((await _unitOfWork.Users.GetAll()).Count(u => u.Id == id) != 0)
                return (await _unitOfWork.Messages.GetAll()).Where(m => m.UserId == id).ToList();
            
            throw new ArgumentException();


        }
    }
}
