using Contracts.CustomArgs;
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
        event EventHandler<CallArgs> CallEvent;
        //event EventHandler<>

        void Call(string number);
        void ConnectToPort(IPort port);
        //void DissconnectFromPort();
        //void ChangePorts(IPort port);
        //string SeeTheNumber();
        //void SeeBalance();
    }
}
