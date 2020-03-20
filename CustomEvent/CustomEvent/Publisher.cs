using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEvent
{
    class Publisher
    {
        public event EventHandler<HelloWorldEvent> RaiseCustomEvent;

        public void DoSomething()
        {
            OnRaiseCustomEvent(new HelloWorldEvent("Hello World"));
        }

        protected virtual void OnRaiseCustomEvent(HelloWorldEvent e)
        {
            EventHandler<HelloWorldEvent> handler = RaiseCustomEvent;

            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
