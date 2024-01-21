using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using password.hashedpassword;
using UserTasks.db;
using UserTasks.Models.Tasks;
using UserTasks.Models.User;

namespace UserTasks.UserServices;


public class UserRepository {

    private UserTasksContext context;
    private HashedPassword hashpassword;

    public UserRepository (UserTasksContext context, HashedPassword hashedPassword) {
        this.context = context;
        this.hashpassword = hashedPassword;
    }

    public async Task<List<Users>> GetUsersAsync () {
        var user = await context.users.ToListAsync<Users>();
        var tasks = await context.userTasks.ToListAsync();
        foreach(var userItem in user){
            foreach (var item in tasks)
            {
                if(userItem.ID == item.UsersId){
                    userItem.Tasks.Add(item);
                }
            }
        }
        return user;
    }
    
    public async Task<Users> CreateUserAsync(Users userDTO) {
        context.users.Add(userDTO);
        await context.SaveChangesAsync();
        return userDTO;
    }

    public async Task<Users> GetUserAsync(string email){
       try
       {
         return await context.users.SingleOrDefaultAsync(data => data.email == email);
       }
       catch (System.Exception e)
       {
        
            throw e;
       }
    }

    public async Task<Users> findOne (int id) {
        Users user = await context.users.SingleAsync(a => a.ID == id);
        var tasks = await context.userTasks.ToListAsync();
        foreach (var item in tasks)
        {
           if(item.UsersId == id){
             user.Tasks.Add(item);
           }
        }
        return  user;
    } 

    public async Task<Users> delete(int id) {
        Users user = await findOne(id);
        context.Remove(await context.users.SingleAsync(a => a.ID == id));
        context.SaveChanges();
        return user;
    }

    public async Task<Users> update (int id, Users updatedUser) {
        Users user = await context.users.SingleAsync(a => a.ID == id);

        Users userDTO = new Users{
            username= updatedUser.username,
            email = updatedUser.email,
            Password = hashpassword.hashedpassword(updatedUser.Password),
        };
    
        user.username = userDTO.username;
        user.email = userDTO.email;
        user.Password = userDTO.Password;
        
        await context.SaveChangesAsync();
        return await Task.FromResult(user);
    }
} 