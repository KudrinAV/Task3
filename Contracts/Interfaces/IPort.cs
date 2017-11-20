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
        StatusOfContract ContractStatus { get; }
        
        void HandleEndCallEvent(object o, EndCallEventArgs e);
        void HandleCallEvent(object o, CallEventArgs e);
        event EventHandler<CallEventArgs> Calling;
        event EventHandler<CallEventArgs> AnswerEvent;
        event EventHandler<EndCallEventArgs> EndingCall;
        event EventHandler<MessageFromAPSEventArgs> MessageFromAPS;
        event EventHandler<BalanceEventArgs> PuttingOnBalance;

       void HandlePutOnBalanceEvent(object o, BalanceEventArgs e);

        void APSMessageShow(MessageFromAPSEventArgs e);
        void GetAnswer(CallEventArgs e);
        void ChangeCallStatus(StatusOfCall status);
        void ChangeStatusOfContract();
        void ChangeStatusOfPort();
        //StatusOfConnect GetAnswer(string number);
    }
}
