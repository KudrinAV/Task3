using Contracts.CustomArgs;
using Contracts.Enums;
using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class Terminal : ITerminal
    {
        public event EventHandler<CallEventArgs> CallEvent;
        public event EventHandler<EndCallEventArgs> EndCallEvent;

        public int Id => throw new NotImplementedException();

        public IPort Port { get; private set; }

        protected virtual void OnEndCall(EndCallEventArgs e)
        {
            EndCallEvent?.Invoke(this, e);
        }

        protected virtual void OnCall(CallEventArgs e)
        {
            CallEvent?.Invoke(this, e);
        }

        public void HandleAnswerEvent(object sender, CallEventArgs e)
        {
            Console.WriteLine("You getting call from" + e.PortOfCaller.Number);
            Console.WriteLine("Y- accept || N- decline");
            string answer = Console.ReadLine();
            e.SetAnswerStatus(answer);
        }

        public void EndCall()
        {
            if (Port.CallStatus == StatusOfCall.OnCall)
            {
                OnEndCall(new EndCallEventArgs(Port));
            }
        }

        public void Call(string number)
        {
            OnCall(new CallEventArgs(Port, number));
        }

        public void ConnectToPort(IPort port)
        {
            Port = port;
            Port.ChangeCallStatus(StatusOfCall.Avaliable);
            Port.ChangeStatusOfPort();
            CallEvent += Port.HandleCallEvent;
            Port.Answer += HandleAnswerEvent;
            EndCallEvent += Port.HandleEndCallEvent;
        }
        
        public string GetNumber()
        {
            if (Port == null) return null;
            return Port.Number;
        }
    }
}
