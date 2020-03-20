using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomEvent
{
    class Program
    {
        public static bool IsPrime(int number)
        {
            for (int i = 2; i < number / 2; i++)
                if (number % i == 0)
                    return false;
            return true;
        }
        public static void LastPrimeNumber(object param)
        {
            int number = (int)param;
            int lastPrimeNumber = 2;
            for(int i = 3; i < number; i++)
            {
                if(IsPrime(i))
                {
                    lastPrimeNumber = i;
                }
            }
            Console.WriteLine(lastPrimeNumber);
        }
        public static void LastPrimeNumberOptim(object param)
        {
            int number = (int)param;
            for (int i = number - 1; i > 2; i--)
            {
                if (IsPrime(i))
                {
                    Console.WriteLine(i);
                    return;
                }
            }
            Console.WriteLine(2);
        }
        static void Main(string[] args)
        {
            Publisher pub = new Publisher();
            Subscriber sub1 = new Subscriber("sub1", pub);
            Subscriber sub2 = new Subscriber("sub2", pub);

            pub.DoSomething();

            int number = 50;
            Thread thread = new Thread(new ParameterizedThreadStart(LastPrimeNumber));
            Thread thread2 = new Thread(new ParameterizedThreadStart(LastPrimeNumberOptim));
            thread.Start(number);
            thread2.Start(number);
        }
    }
}
