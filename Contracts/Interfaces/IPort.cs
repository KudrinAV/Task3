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
        StatusOfPort PortStatus { get;}

        int GetIdOfTerminal();
        void ChangeStatus();
    }
}
