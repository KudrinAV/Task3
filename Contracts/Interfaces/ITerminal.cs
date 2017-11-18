using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface ITerminal
    {
        int Id { get; }
        IPort Port { get; }
        event EventHandler<CallArgs> Call;
        event EventHandler<AnswerArgs> Answer;
        event EventHandler<EndCallArgs> EndCall;
        //event EventHandler<>

        void ConnectToPort(IPort port);
        void DissconnectFromPort();
        void ChangePorts(IPort port);
        void SeeTheNumber();
        void SeeBalance();
    }
}
