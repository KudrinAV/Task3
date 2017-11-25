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
        public IPort Reciever { get; private set; }
        public StatusOfAnswer Answer { get; private set; }

        public AnswerEventArgs(string number, IPort port, StatusOfAnswer answer)
        {
            CallingNumber = number;
            Reciever = port;
            Answer = answer;
        }


    }
}
