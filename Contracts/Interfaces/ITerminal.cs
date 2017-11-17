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
        event EventHandler<Args> CallEvent;
        event EventHandler<Args> EndCall;
        //event EventHandler<>

        void ConnectToPort(IPort port);
        void DissconnectFromPort();
        void ChangePorts(IPort port);
        void Call(string number);
        void Answer();
        void Abort();
        void SeeTheNumber();
        void SeeBalance();
    }
}
