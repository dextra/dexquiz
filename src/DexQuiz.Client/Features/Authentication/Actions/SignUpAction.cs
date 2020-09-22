using BlazorState;
using DexQuiz.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Authentication
{
    public partial class AuthState
    {
        public class SignUpAction : IAction
        {
            public UserModel Data { get; private set; }

            public SignUpAction(UserModel data)
            {
                Data = data;
            }
        }
    }
}
