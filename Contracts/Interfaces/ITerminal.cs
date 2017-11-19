﻿using Contracts.CustomArgs;
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
        event EventHandler<CallEventArgs> CallEvent;
        event EventHandler<EndCallEventArgs> EndCallEvent;

        void HandleAnswerEvent(object o, CallEventArgs e);
        void HandleMessageFromAPSEvent(object o, MessageFromAPSEventArgs e);
        void EndCall();
        string GetNumber();
        void Call(string number);
        void ConnectToPort(IPort port);
        void DissconnectFromPort();
        //string SeeTheNumber();
        //void SeeBalance();
    }
}
