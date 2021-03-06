﻿using DexQuiz.Core.Entities;
using DexQuiz.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DexQuiz.Core.Interfaces.Services
{
    public interface IUserService
    {
        Task<ProcessResult> AddUser(User user);
        Task<User> FindUserById(int id);
        Task<ProcessResult> RemoveAccount(int userId);
    }
}
