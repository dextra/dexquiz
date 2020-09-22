using BlazorState;
using DexQuiz.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DexQuiz.Client.Features.Track
{
    public partial class TrackState
    {
        public class AnswerQuestionAction : IAction
        {
            public AnswerQuestionAction(int trackId, QuestionAnswerModel answer)
            {
                Answer = answer;
                TrackId = trackId;
            }

            public int TrackId { get; private set; }

            public QuestionAnswerModel Answer { get; private set; }
        }
    }
}
