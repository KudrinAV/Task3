using Contracts.Enums;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.CustomArgs
{
    public class AnswerEventArgs : EventArgs
    {
        public string CallingNumber { get; private set; }
        public string RecieverNumber { get; private set; }
        public StatusOfAnswer Answer { get; private set; }

        public AnswerEventArgs(string callingNumber, string number, StatusOfAnswer answer)
        {
            CallingNumber = callingNumber;
            RecieverNumber = number;
            Answer = answer;
        }


    }
}
