using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Learning_Events
{
    public class CustomEventArgs : EventArgs
    {
        public CustomEventArgs(string s)
        {
            Message = s;
            number = 10;
        }
        public int number { get; private set; }
        public string Message { get; set; }
    }
}
