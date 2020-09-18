using BlazorState;
using DexQuiz.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Dashboard
{
    public partial class DashboardState : State<DashboardState>
    {
        public bool IsLoading { get; private set; }
        public string Message { get; private set; }
        public bool HasError { get; private set; }
        public TrackModel[] Tracks { get; set; }
        public TrackModel CurrentTrack { get; private set; }
        public TrackRakingModel[] CurrentTrackRakings { get; private set; }

        public override void Initialize()
        {
            IsLoading = false;
            HasError = false;
            Message = null;
            Tracks = new TrackModel[0];
            CurrentTrack = null;
            CurrentTrackRakings = new TrackRakingModel[0];
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
