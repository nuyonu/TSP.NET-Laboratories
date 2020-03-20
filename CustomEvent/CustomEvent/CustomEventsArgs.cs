using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomEvent
{
    public class HelloWorldEvent : EventArgs
    {
        public HelloWorldEvent(string s)
        {
            Message = s;
        }
        public string Message { set; get; }
    }
}
