using Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CustomArgs
{
    public class CallEventArgs : EventArgs
    {
        public string ReceivingNumber { get; private set; }
        public string CallerNumber { get; private set; }
        public StatusOfAnswer AnswerStatus { get; private set; }

        public CallEventArgs(string caller, string reciver)
        {
            CallerNumber = caller;
            ReceivingNumber = reciver;
        }

        public void SetAnswerStatus(string answer)
        {
            if (answer == "y")
            {
                AnswerStatus = StatusOfAnswer.Answer;
            }
            else AnswerStatus = StatusOfAnswer.Decline;
        }
    }
}
