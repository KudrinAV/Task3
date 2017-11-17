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
        bool GetStatus();
        bool Call();
        void Abort();
        void SeeTheNumber();
        void SeeBalance();
    }
}
