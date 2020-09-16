using BlazorState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features
{
    public abstract class BaseState<T> : State<T>
    {
        public bool Loading { get; private set; }
        public bool Success { get; private set; }
        public string Message { get; private set; }

        protected void StartLoading()
        {
            Loading = true;
            Message = string.Empty;
        }

        protected void Succeed(string message = null)
        {
            Loading = false;
            Success = true;
            Message = message;
        }

        protected void Fail(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException("If failed, the message is required.");

            Loading = false;
            Success = false;
            Message = message;
        }

        public override void Initialize()
        {
            Loading = false;
            Success = false;
            Message = string.Empty;
        }
    }
}
