using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface ICallInformation
    {
        string Caller { get; }
        string Receiver { get; }
        DateTime TimeOfBeginningOfCall { get; }
        DateTime TimeOfEndingOfCall { get; }
        double CostOfCall { get;  }
        
        void SetCostOfCall(double cost);
        void SetTimeOfEnding(DateTime time);
        TimeSpan GetDuretionOfCall();
    }
}
