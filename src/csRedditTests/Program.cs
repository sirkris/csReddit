using csReddit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csRedditTests
{
    class Program
    {
        private static string username = "csRedditTest";
        private static string password = "csReddit";

        static void Main(string[] args)
        {
            RunTests();
        }

        private static void RunTests()
        {
            Reddit reddit = new Reddit(false);

            if (reddit.Account.Login(username, password))
            {
                Console.WriteLine("Login successful!");
            }
            else
            {
                Console.WriteLine("Login failed : " + reddit.Account.error);
                return;
            }


        }
    }
}
