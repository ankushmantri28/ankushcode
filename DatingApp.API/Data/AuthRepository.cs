using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context=context;
        }
        public async Task<Users> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=>x.Username==username);
            if(user==null){
                return null;
            }
            if(!VerifyHashPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;
                
                return user;
        }

        private bool VerifyHashPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
             using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {                
                var computedHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0; i< computedHash.Length;i++)
                {
                    if(computedHash[i]!=passwordHash[i]) return false;
                }
            }
            return true;   
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt=hmac.Key;
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }           

            //throw new NotImplementedException();
        }

        public async Task<Users> Register(Users users, string password)
        {
            byte[] passwordHash,passwordSalt;
            CreatePasswordHash(password,out passwordHash,out passwordSalt);
            users.PasswordHash=passwordHash;
            users.PasswordSalt=passwordSalt;

            await _context.Users.AddAsync(users);
            await _context.SaveChangesAsync();
            return users;
        }

        public async Task<bool> UsersExists(string usersname)
        {
           if(await _context.Users.AnyAsync(x=>x.Username==usersname))
            return true;
            else
            return false;
        }
    }
}