using System;
using Data;
using Entities;
using CustomizeException;

namespace BussinesClass
{
    /// <summary>
    /// Class to manage the User entity and db table
    /// </summary>
    /// <returns></returns>
    public class UserBusiness
    {
        private UserDataClass userManager = null;
        public UserBusiness()
        {
            userManager = new UserDataClass();
        }

        public User Login(string userName, string password)
        {
            User userLoginData = userManager.GetUser(user: userName, password: password);
            if (userLoginData == null)
            {
                throw new UserOrPasswordIncorrectException();
            }
            return userLoginData;

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
                throw new UserTakenException();
            }

        }
    }
}
