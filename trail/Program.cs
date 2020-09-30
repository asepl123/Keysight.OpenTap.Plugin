using System;
using System.Collections.Generic;

namespace Trial
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine(new BalancedParanthesis().myfunc("("));
            Console.WriteLine(new BalancedParanthesis().myfunc("([])"));
            Console.WriteLine(new BalancedParanthesis().myfunc("([)]"));
            Console.WriteLine(new BalancedParanthesis().myfunc(")("));
        }

    }

    
}
