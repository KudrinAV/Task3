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
        public string Caller { get; private set; }
        public string Receiver { get; private set; }
        public DateTime TimeOfBeginningOfCall { get; private set; }
        public DateTime TimeOfEndingOfCall { get; private set; }
        public double CostOfCall { get; private set; }

        public TimeSpan GetDuretionOfCall()
        {
            TimeSpan res = TimeOfEndingOfCall.Subtract(TimeOfBeginningOfCall);
            return res;
        }

        public void SetCostOfCall(double cost)
        {
            CostOfCall = cost;
        }

        public void SetTimeOfEnding(DateTime time)
        {
            TimeOfEndingOfCall = time;
        }

        public CallInformation(string caller, string receiver)
        {
            Caller = caller;
            Receiver = receiver;
            TimeOfBeginningOfCall = DateTime.Now;
        }
    }
}
