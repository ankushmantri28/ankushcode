using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IAuthRepository
    {
         Task<Users> Register(Users Users,string password);
         Task<Users> Login(string username,string password);
         Task<bool> UsersExists(string usersname);
         
    }
}