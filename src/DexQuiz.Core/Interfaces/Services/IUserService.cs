using DexQuiz.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DexQuiz.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> AddUser(User user);
        Task<User> FindUserById(int id);
    }
}
