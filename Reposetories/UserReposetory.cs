using Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
namespace Reposetories

{
    public class UserReposetory :  IUserReposetory
    {
        MyShop214935017Context myShop;
        public UserReposetory(MyShop214935017Context myShop)
        {
            this.myShop = myShop;
        }
        List<User> users = new();//
        //public async Task<User> Get()
        //{
        //    await myShop.Users.GetAsyncEnumerator()
        //    return users;


        //}
        public async Task<User> Get(int id)
        {
            User user = await myShop.Users.FindAsync(id);
            return user;



        }
        public async Task<User> Post(User user)
        {
            await myShop.Users.AddAsync(user);
            //var res=await myShop.Users.AddAsync(user);
            await myShop.SaveChangesAsync();
            //return res- the created user with the id
            return user;

        }
        public async Task<User> Login(string email, string password)
        {
            User currentUser = await myShop.Users.FirstOrDefaultAsync(user=> user.Password==password&& user.Email==email); //await myShop.Users.FirstOrDefault(currentUser=> currentUser.Password == password && currentUser.Email==email);
            return currentUser;

            
            

        }
        public async Task<User> Put(int id, User userToUpdate)
        {
       
            myShop.Users.Update(userToUpdate);
            await myShop.SaveChangesAsync();
            return userToUpdate;

        }
    }
}
