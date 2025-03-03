using Reposetories;
using System.Text.Json;

using Entities;
using Zxcvbn;
using Microsoft.Extensions.Logging;
using DTO;


namespace Servicess
{
    public class UserServicess : IUserServicess
    {
        IUserReposetory resposetory;
       private readonly ILogger<UserServicess> logger;

        public UserServicess(IUserReposetory resposetory, ILogger<UserServicess> logger)
        {
            this.resposetory = resposetory;
            this.logger = logger;
        }

        public async Task<User> Get(int id)
        {

            return await resposetory.Get(id);
        }
        public async Task<User> Post(User user)
        {
            int check = CheckPassword(user.Password);
            if (check >= 3)
                return await resposetory.Post(user);
            return null;
        }
        public async Task<User> Login(string email, string password)
        {
            User user =await resposetory.Login(email, password);
          logger.LogInformation($"{user.Id}, {user.Email}, {user.FirstName}, {user.LastName} login to app!!");
            return user;

        }
        public async Task<User> Put(int id, User userToUpdate)
        {
            int check = CheckPassword(userToUpdate.Password);
            if (check >= 3)
                return await resposetory.Put(id, userToUpdate);
            return null;

        }
        public int CheckPassword(string password)
        {
            var result = Zxcvbn.Core.EvaluatePassword(password);

            return result.Score;
        }

    }


}

