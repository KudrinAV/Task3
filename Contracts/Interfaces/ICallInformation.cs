using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface ICallInformation
    {
        IPort Caller { get; }
        IPort Receiver { get; }
        DateTime TimeOfBeginningOfCall { get; }
        DateTime TimeOfEndingOfCall { get; }

        void SetTimeOfEnding(DateTime time);
        TimeSpan GetDuretionOfCall();

    }
}
