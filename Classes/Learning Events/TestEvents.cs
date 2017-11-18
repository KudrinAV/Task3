using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes.Learning_Events
{
    public class TestEvents
    {
        public event EventHandler<CustomEventArgs> TestEvent;

        public Subscriber subscriber;

        public void ChangeS()
        {
            Console.Write("kek");
            OnTestEvent(new CustomEventArgs("new data"));
        }

        protected virtual void OnTestEvent(CustomEventArgs e)
        {
            EventHandler<CustomEventArgs> handler = TestEvent;
            if(handler != null)
            {
                e.Message += String.Format("at {0}", DateTime.Now.ToString());
                handler(this, e);
            }
        }

        public TestEvents()
        {
            TestEvent += subscriber.HandleCustomEvent;
        }
    }
}
