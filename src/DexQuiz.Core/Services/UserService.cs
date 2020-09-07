using DexQuiz.Core.Entities;
using DexQuiz.Core.Interfaces.Repositories;
using DexQuiz.Core.Interfaces.Services;
using DexQuiz.Core.Interfaces.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexQuiz.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<bool> AddUser(User user)
        {
            try
            {
                if (!await IsEmailAvailable(user.Email)) 
                    throw new Exception("Já existe um cadastro com esse e-mail");
                
                if (!await IsCellPhoneAvailable(user.CellPhone)) 
                    throw new Exception("Já existe um cadastro com esse celular");

                await _userRepository.AddAsync(user);
                await _unitOfWork.CommitAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<User> FindUserById(int id) =>
            await _userRepository.FindAsync(id);

        private async Task<bool> IsEmailAvailable(string userEmail) => 
            await _userRepository.GetUserByEmailAsync(userEmail) == null;

        private async Task<bool> IsCellPhoneAvailable(string userCellPhone) => 
            !(await _userRepository.FindAsync(x => x.CellPhone == userCellPhone)).Any();
    }
}
