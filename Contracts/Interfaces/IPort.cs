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

        void HandleChangeTariffEvent(object o, ChangeTariffEventArgs e);
        void HandleEndCallEvent(object o, EndCallEventArgs e);
        void HandleCallEvent(object o, CallEventArgs e);
        event EventHandler<ChangeTariffEventArgs> ChangingTariff;
        event EventHandler<AnswerEventArgs> Connecting;
        event EventHandler<CallEventArgs> Calling;
        event EventHandler<CallEventArgs> AnswerEvent;
        event EventHandler<EndCallEventArgs> EndingCall;
        event EventHandler<MessageFromAPSEventArgs> MessageFromAPS;
        event EventHandler<BalanceEventArgs> PuttingOnBalance;
        event EventHandler<GetHistoryEventArgs> GettingHistory;

        void HandleConnectEvent(object o, AnswerEventArgs e);
        void HandlePutOnBalanceEvent(object o, BalanceEventArgs e);
        void HandleGetHistoryEvent(object o, GetHistoryEventArgs e);
        void APSMessageShow(MessageFromAPSEventArgs e);
        void GetAnswer(CallEventArgs e);
        void ChangeCallStatus(StatusOfCall status);
        void ChangeStatusOfContract();
        void ChangeStatusOfPort();
        //StatusOfConnect GetAnswer(string number);
    }
}
