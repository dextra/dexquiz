using DexQuiz.Core.Entities;
using DexQuiz.Core.Exceptions;
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

        public async Task<ReturnData> AddUser(User user)
        {
            try
            {
                if (!await IsEmailAvailable(user.Email)) 
                    return new ReturnData { Message = "Já existe um cadastro com esse e-mail", Result = false };
                
                if (!await IsCellPhoneAvailable(user.CellPhone))
                    return new ReturnData { Message = "Já existe um cadastro com esse celular", Result = false };

                await _userRepository.AddAsync(user);
                await _unitOfWork.CommitAsync();

                return new ReturnData { Message = "Usuário adicionado com sucesso", Result = true };
            }
            catch (Exception e)
            {
                return new ReturnData { Message = e.Message, Result = false };
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
