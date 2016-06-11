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

        private static string sub = "csReddit";

        private static Reddit reddit;

        static void Main(string[] args)
        {
            RunTests();
        }

        private static void RunTests()
        {
            reddit = new Reddit(false);

            /* Login to Reddit.  --Kris */
            if (reddit.Account.Login(username, password))
            {
                Console.WriteLine("Login successful!");
            }
            else
            {
                Console.WriteLine("Login failed : " + reddit.Account.error);
                return;
            }
            
            /* Search for link posts with a certain flair.  --Kris */
            // Results:  res["data"]["children"][i], where i is 0-indexed index for each result.  --Kris
            dynamic res = SearchForFlair("csReddit Test Flair", false, sub);

            /* Search for self posts with a certain flair.  --Kris */
            dynamic res2 = SearchForFlair("csReddit Test Flair", true, sub);

            // TODO - Finish/test below when the test user can post without captcha.  --Kris
            return;

            /* Submit a new link post.  --Kris */
            dynamic linkPost = reddit.LinksAndComments.submit("", "", "", "link", true, true, sub, "http://www.reddit.com/u/" + username, "Test Link Post");

            /* Delete the link post.  --Kris */


            /* Submit a new self post.  --Kris */


            /* Edit the text of the self post.  --Kris */


            /* Submit a new comment to the self post.  --Kris */



        }

        private static dynamic SearchForFlair(string flair, bool self = false, string sub = "", bool verbose = true)
        {
            dynamic res = reddit.Search.search(null, null, "flair:\"" + flair + "\" self:" + (self ? "yes" : "no"), false, "new", null, null, sub);

            if (verbose)
            {
                if (res["data"]["children"] == null || res["data"]["children"].Count == 0)
                {
                    Console.WriteLine("No link posts with the csReddit test flair found in /r/" + sub + "!");
                }
                else
                {
                    Console.WriteLine("Found " + res["data"]["children"].Count.ToString() + " results:");

                    foreach (dynamic o in res["data"]["children"])
                    {
                        if (o != null)
                        {
                            Console.WriteLine("Post title:  " + o["data"]["title"] + " [" + o["data"]["url"] + "] (" + o["data"]["score"].ToString() + ")");
                            if (o["data"]["selftext"] != null
                                && (self == true || o["data"]["selftext"]!= ""))
                            {
                                Console.WriteLine(@"{");
                                Console.WriteLine((string) o["data"]["selftext"]);
                                Console.WriteLine(@"}");
                            }
                        }
                    }
                }
            }

            return res;
        }
    }
}
