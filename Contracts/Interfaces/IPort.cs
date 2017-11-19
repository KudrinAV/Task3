using Contracts.CustomArgs;
using Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IPort
    {
        int Id { get; }
        string Number { get; }
        StatusOfPort PortStatus { get; }
        StatusOfCall CallStatus { get; }

        void HandleEndCallEvent(object o, EndCallEventArgs e);
        void HandleCallEvent(object o, CallEventArgs e);
        event EventHandler<CallEventArgs> Calling;
        event EventHandler<CallEventArgs> Answer;
        event EventHandler<EndCallEventArgs> EndingCall;

        void GetAnswer(CallEventArgs e);
        void ChangeCallStatus(StatusOfCall status);
        //int GetIdOfTerminal();
        void ChangeStatusOfPort();
        //StatusOfConnect GetAnswer(string number);
    }
}
