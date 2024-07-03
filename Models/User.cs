

namespace ConsoleProjectCodeAcademy.Models;

public class User: BaseEntity
{
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public User(string fullname, string email, string password)
    {
        Fullname = fullname;
        Email = email;
        Password = password;
    }

}
