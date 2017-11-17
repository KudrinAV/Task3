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

        void ConnectToPort(IPort port);
        void DissconnectFromPort();
        void ChangePorts(IPort port);
        bool GetStatus();
        void Call(string number);
        void Answer();
        void Abort();
        void SeeTheNumber();
        void SeeBalance();
    }
}
