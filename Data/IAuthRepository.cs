﻿using SehirRehberiAPI.Models;
using System.Threading.Tasks;

namespace SehirRehberiAPI.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userName, string password);
        Task<bool> UserExists(string userName);
    }
}
