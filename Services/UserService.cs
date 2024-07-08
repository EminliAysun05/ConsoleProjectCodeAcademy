

using ConsoleProjectCodeAcademy.Exceptions;
using ConsoleProjectCodeAcademy.Models;
using System.Reflection.Metadata.Ecma335;

namespace ConsoleProjectCodeAcademy.Services;

public class UserService
{
    public User Login(string email, string password)
    {
        //db user arrayini yoxlayir
        foreach (var user in DB.Users)
        {
            if (email == user.Email && password == user.Password)
            {
                Console.WriteLine("Registration succesfully...");
                return user;
            }
        }
        throw new NotFoundException("User not found...");

    }
    public void AddUser(User user)
    {


        foreach (var u in DB.Users)
        {
            if(u.Email==user.Email)
            {
                throw new AlreadyExistsException("This email is already exist");
            }
        }
        Array.Resize(ref DB.Users, DB.Users.Length + 1);
        DB.Users[DB.Users.Length - 1] = user;
    }

}
