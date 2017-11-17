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
        StatusOfCall CallStatus { get; }

        void GetAPS(IAPS aps);
        void Call(string number);
        void ChangeCallStatus(StatusOfCall status);
        int GetIdOfTerminal();
        void ChangeStatus();
        StatusOfConnect GetAnswer(string number);
    }
}
