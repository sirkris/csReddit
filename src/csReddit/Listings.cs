using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csReddit
{
    public class Listings
    {
        public string error;
        public string warning;

        private Account Account;
        public Listings(Account Account)
        {
            this.Account = Account;
        }

        public Listings() : this(null) { }

        public Dictionary<string, string> comments(string article, string comment, string context, int depth, int limit, string sort, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com"
                + (subreddit != "" ? @"/r/" + subreddit : "") + @"/comments/" + article + @".json", 
                @"comment=" + comment + @"&context=" + context + @"&depth=" + depth.ToString() + @"&limit=" + limit.ToString() + @"&sort=" + sort, 
                Account.cookies, Account.authheaders);

            if (ret["StatusCode"] == "200")
            {
                if (Account.CheckValidation(REST.ValidateReturnData(ret)) == true)
                {
                    return REST.json_decode(REST.json_prepare(ret["Body"]));
                }
                else
                {
                    return new Dictionary<string, string>();
                }
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return new Dictionary<string, string>();
            }
        }

        public Dictionary<string, string> hot(string after, string before, int count, int limit, string show, string target, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com"
                + (subreddit != "" ? @"/r/" + subreddit : "") + @"/hot.json", 
                @"after=" + after + @"&before=" + before + @"&count=" + count.ToString() + @"&limit=" + limit.ToString() + @"&show=" + show + @"&target=" + target,
                Account.cookies, Account.authheaders);

            if (ret["StatusCode"] == "200")
            {
                if (Account.CheckValidation(REST.ValidateReturnData(ret)) == true)
                {
                    return REST.json_decode(REST.json_prepare(ret["Body"]));
                }
                else
                {
                    return new Dictionary<string, string>();
                }
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return new Dictionary<string, string>();
            }
        }

        // Sorry, but "new" is a reserved keyword in C#.  --Kris
        public Dictionary<string, string> new_list(string after, string before, int count, int limit, string show, string target, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com"
                + (subreddit != "" ? @"/r/" + subreddit : "") + @"/new.json", 
                @"after=" + after + @"&before=" + before + @"&count=" + count.ToString() + @"&limit=" + limit.ToString() + @"&show=" + show + @"&target=" + target, 
                Account.cookies, Account.authheaders);

            if (ret["StatusCode"] == "200")
            {
                if (Account.CheckValidation(REST.ValidateReturnData(ret)) == true)
                {
                    return REST.json_decode(REST.json_prepare(ret["Body"]));
                }
                else
                {
                    return new Dictionary<string, string>();
                }
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return new Dictionary<string, string>();
            }
        }

        /*
         * This function returns a raw HTML string dump of a random subreddit or post page!
         * Unfortunately, the Reddit API does not offer a way to just get the name of a random subreddit.
         * 
         * TODO - Parse the raw garbage returned by the API and just return the name of the sub/post/whatever.
         * 
         * --Kris
         */
        public string random(string subreddit = "")
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com"
                + (subreddit != "" ? @"/r/" + subreddit : "") + @"/random",
                "", Account.cookies, Account.authheaders);

            if (ret["StatusCode"] == "200")
            {
                return ret["Body"];
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return "";
            }
        }

        public Dictionary<string, string> top(string t, string after, string before, int count, int limit, string show, string target, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com"
                + (subreddit != "" ? @"/r/" + subreddit : "") + @"/top.json",
                @"t=" + t + @"after=" + after + @"&before=" + before + @"&count=" + count.ToString() + @"&limit=" + limit.ToString()
                + @"&show=" + show + @"&target=" + target,
                Account.cookies, Account.authheaders);

            if (ret["StatusCode"] == "200")
            {
                if (Account.CheckValidation(REST.ValidateReturnData(ret)) == true)
                {
                    return REST.json_decode(REST.json_prepare(ret["Body"]));
                }
                else
                {
                    return new Dictionary<string, string>();
                }
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return new Dictionary<string, string>();
            }
        }

        public Dictionary<string, string> controversial(string t, string after, string before, int count, int limit, string show, string target, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com"
                + (subreddit != "" ? @"/r/" + subreddit : "") + @"/controversial.json",
                @"t=" + t + @"after=" + after + @"&before=" + before + @"&count=" + count.ToString() + @"&limit=" + limit.ToString()
                + @"&show=" + show + @"&target=" + target,
                Account.cookies, Account.authheaders);

            if (ret["StatusCode"] == "200")
            {
                if (Account.CheckValidation(REST.ValidateReturnData(ret)) == true)
                {
                    return REST.json_decode(REST.json_prepare(ret["Body"]));
                }
                else
                {
                    return new Dictionary<string, string>();
                }
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return new Dictionary<string, string>();
            }
        }
    }
}
