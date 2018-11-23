using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Entities;

namespace BussinesClass
{
    public class UserBusiness
    {
        private UserDataClass userManager = null;
        public UserBusiness()
        {
            userManager = new UserDataClass();
        }

        public User Login(string userName, string password)
        {
            return userManager.GetUser(user: userName, password: password);
            //todo agregar inicio de seccion en la tabla user seccion
        }

        public bool CreateNewUser(string name, string lastName, DateTime birthDay, string email, string userName, string password, string department)
        {
   
            if (userManager.GetUser(user: userName) == null)
            {
                User newUser = new User();
                newUser.Name = name;
                newUser.LastName = lastName;
                newUser.Birthday = birthDay;
                newUser.Email = email;
                newUser.UserName = userName;
                newUser.Password = password;
                newUser.CompanyDepartment = department;
                userManager.CreateUser(newUser);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
