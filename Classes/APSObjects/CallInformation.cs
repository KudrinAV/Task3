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
        public DateTime BeginningOfCall { get; private set; }
        public DateTime EndingOfCall { get; private set; }
        public double CostOfCall { get; private set; }

        public TimeSpan GetDuretionOfCall()
        {
            TimeSpan res = EndingOfCall.Subtract(BeginningOfCall);
            return res;
        }

        public void SetCostOfCall(double cost)
        {
            CostOfCall = cost;
        }

        public void SetTimeOfEnding(DateTime time)
        {
            EndingOfCall = time;
        }

        public CallInformation(string caller, string receiver)
        {
            Caller = caller;
            Receiver = receiver;
            BeginningOfCall = DateTime.Now;
        }
    }
}
