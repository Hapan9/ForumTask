using System;
using System.Collections.Generic;
using System.Text.Json;
using DAL.Models;
using DAL;
using DAL.Enums;
using System.Linq;

namespace BLL
{
    public class WorkWithUser
    {
        UnitOfWork unitOfWork = new UnitOfWork();

        public void UserRegustration(string _name, string _surname, string _login, string _password)
        {
            if (_login.Length < 4 || _password.Length < 4 || _name.Length < 4)
                return;
            else if (_surname != null && _surname.Length < 4)
                return;

            unitOfWork.Users.Create(new User() { Name = _name, Surname = _surname, Login = _login, Password = Hashing.GetHashString(_password), Role = Roles.User });
        }

        public string GetUsers()
        {
            return JsonSerializer.Serialize<IEnumerable<User>>((IEnumerable<User>)unitOfWork.Users.GetAll());
        }

        public string GetUser(Guid _id)
        {
            return JsonSerializer.Serialize<User>((User)unitOfWork.Users.Get(_id));
        }

        public void UpadteUser(Guid _id, string _name, string _surname, string _login, string _password, int _role)
        {
            if (unitOfWork.Users.Get(_id) == null)
                return;
            else if (_login.Length < 4 || _password.Length < 4 || _name.Length < 4)
                return;
            else if (_surname != null && _surname.Length < 4)
                return;

            foreach (var enumtype in Enum.GetValues(typeof(Roles)))
                if ((int)enumtype == _role)
                {
                    unitOfWork.Users.Update(new User() {Id = _id, Name = _name, Surname = _surname, Login = _login, Password = Hashing.GetHashString(_password), Role = (Roles)enumtype });
                    break;
                }
        }

        public void DeleteUser(Guid _id)
        {
            if (unitOfWork.Users.Get(_id) == null)
                return;

            unitOfWork.Users.Delete(_id);
        }

        public string CheckUserEnter(string _login, string _password)
        {
            if (((IEnumerable<User>)unitOfWork.Users.GetAll()).Where(u => u.Login == _login).Count() == 0)
                return null;

            var user = ((IEnumerable<User>)unitOfWork.Users.GetAll()).Where(u => u.Login == _login).First();

            if (user.Password != Hashing.GetHashString(_password))
                return null;

            return JsonSerializer.Serialize<User>(user);
        }
    }
}
