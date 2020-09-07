using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DexQuiz.Core.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<string> GenerateAuthenticationAsync(string userEmail);
        Task<bool> ValidateCredentialsAsync(string email, string password);
    }
}
