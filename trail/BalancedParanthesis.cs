using System;
using System.Collections.Generic;
using System.Text;

namespace Trial
{
    public class BalancedParanthesis
    {
        bool res;

        public bool myfunc(string exp)
        {
            List<char> mystack = new List<char>();
            bool result = false;

            foreach (var item in exp)
            {
                if (mystack.Count == 0 && ")}]".Contains(item))
                {
                    result = false;
                    break;
                }
                else
                {
                    switch (item)
                    {
                        case '(':
                        case '{':
                        case '[':
                            mystack.Add(item);
                            Console.WriteLine(mystack.ToString(), " ", mystack.Count);
                            result = false;
                            break;

                        case ')' when mystack[mystack.Count - 1] == '(' && mystack.Count != 0:
                        case '}' when mystack[mystack.Count - 1] == '{' && mystack.Count != 0:
                        case ']' when mystack[mystack.Count - 1] == '[' && mystack.Count != 0:
                            mystack.RemoveAt(mystack.Count - 1);
                            Console.WriteLine(mystack.ToString(), " ", mystack.Count);
                            result = true;
                            break;
                        default:
                            result = false;
                            break;
                    }
                }
            }

            res = result && (mystack.Count == 0);
            Console.WriteLine(exp);
            Console.WriteLine(res);
            return res;
        }
    }
}
