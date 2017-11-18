using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Learning_Events
{
    public class Subscriber
    {
        public string id;
        public Subscriber(string ID, TestEvents test)
        {
            id = ID;
            test.TestEvent += HandleCustomEvent;
        }

        void HandleCustomEvent(object o, CustomEventArgs e)
        {
            Console.WriteLine(id + " recived this message: {0}", e.Message);
        }
    }
}
