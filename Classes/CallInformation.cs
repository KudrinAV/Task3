using Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class CallInformation : ICallInformation
    {
        public IPort Caller { get; private set; }
        public IPort Receiver { get; private set; }
        public DateTime TimeOfBeginningOfCall { get; private set; }
        public DateTime TimeOfEndingOfCall { get; private set; }

        public TimeSpan GetDuretionOfCall()
        {
            TimeSpan res = TimeOfEndingOfCall.Subtract(TimeOfEndingOfCall);
            return res;
        }

        public void SetTimeOfEnding(DateTime time)
        {
            TimeOfEndingOfCall = time;
        }

        public CallInformation(IPort caller, IPort receiver)
        {
            Caller = caller;
            Receiver = receiver;
            TimeOfBeginningOfCall = DateTime.Now;
        }
    }
}
