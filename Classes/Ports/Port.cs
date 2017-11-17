using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Enums;
using Contracts.Interfaces;

namespace Classes.Ports
{
    public class Port : IPort
    {
        public string Number { get; private set; }

        public int Id { get; private set; }

        public StatusOfPort PortStatus { get; private set; }

        public StatusOfCall CallStatus { get; private set; }

        public void ChangeCallStatusToAvaliable()
        {
            CallStatus = StatusOfCall.Avaliable;
        }

        public void ChangeCallStatusToOnCall()
        {
            CallStatus = StatusOfCall.OnCall;
        }

        public void ChangeCallStatusToNotAvaliable()
        {
            CallStatus = StatusOfCall.NotAvalibale;
        }

        public int GetIdOfTerminal()
        {
            return Id;
        }
        
        public void ChangeStatus()
        {
            PortStatus = StatusOfPort.NotConnected == PortStatus ? StatusOfPort.Connected : StatusOfPort.NotConnected;
        }

        public Port(string numberOfTerminal, int id)
        {
            Number = numberOfTerminal;
            Id = id;
            PortStatus = StatusOfPort.NotConnected;
            CallStatus = StatusOfCall.NotAvalibale;
        }
    }
}
