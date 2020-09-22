using BlazorState;
using DexQuiz.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Track
{
    public partial class TrackState : State<TrackState>
    {
        public bool IsLoading { get; private set; }
        public string Message { get; private set; }
        public bool HasError { get; private set; }
        public TrackModel Track { get; private set; }
        public int TotalQuestions { get; private set; }
        public int RemainingQuestions { get; private set; }
        public QuestionModel Question { get; private set; }
        public TrackRankingModel TrackResult { get; private set; }

        public override void Initialize()
        {
            IsLoading = true;
            HasError = false;
            Message = null;
            Track = null;
            TotalQuestions = 12;
            RemainingQuestions = 0;
            Question = new QuestionModel();
            TrackResult = new TrackRankingModel();
        }

        public void InitializeWithProgress()
        {
            var remainingQuestions = RemainingQuestions;
            Initialize();
            RemainingQuestions = remainingQuestions;
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
