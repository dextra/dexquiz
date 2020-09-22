using BlazorState;
using DexQuiz.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Authentication
{
    public partial class AuthState : State<AuthState>
    {
        public bool IsLoading { get; private set; }
        public string Message { get; private set; }
        public bool HasError { get; private set; }
        public ProfileModel User { get; private set; }

        public override void Initialize()
        {
            User = null;
            IsLoading = false;
            HasError = false;
            Message = null;
        }

        private void StartLoading()
        {
            Message = "Processando";
            HasError = false;
            IsLoading = true;
        }

        private void Succeed(string message = null)
        {
            Message = message;
            HasError = false;
            IsLoading = false;
        }

        private void Fail(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException("message");
            }

            Message = message;
            HasError = true;
            IsLoading = false;
        }
    }
}
