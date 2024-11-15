﻿using Entities;
using Reposetories;
using System.Text.Json;
using Zxcvbn;



namespace Servicess
{
    public class UserServicess : IUserServicess
    {
        IUserReposetory resposetory;

        public UserServicess(IUserReposetory resposetory)
        {
            this.resposetory = resposetory;
        }

        public User Get(int id)
        {

            return resposetory.Get(id);
        }
        public User Post(User user)
        {
            int check = CheckPassword(user.Password);
            if (check >= 3)
                return resposetory.Post(user);
            return null;
        }
        public User Login(string email, string password)
        {


            return resposetory.Login(email, password);

        }
        public User Put(int id, User userToUpdate)
        {
            int check = CheckPassword(userToUpdate.Password);
            if (check >= 3)
                return resposetory.Put(id, userToUpdate);
            return null;

        }
        public int CheckPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);

            return result.Score;
        }

    }
}

