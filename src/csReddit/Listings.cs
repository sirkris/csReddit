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

        public dynamic comments(string article, string comment, string context, int depth, int limit, string sort, string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/comments/" + article + @".json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "comment", "context", "depth", "limit", "sort", "api_type" },
                new object[] { comment, context, depth.ToString(), limit.ToString(), sort, "json" });
        }

        public dynamic hot(string after, string before, int count, int limit, string show, string target, string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/hot.json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "after", "before", "count", "limit", "show", "target", "api_type" },
                new object[] { after, before, count.ToString(), limit.ToString(), show, target, "json" });
        }

        // Sorry, but "new" is a reserved keyword in C#.  --Kris
        public dynamic new_list(string after, string before, int count, int limit, string show, string target, string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/new.json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "after", "before", "count", "limit", "show", "target", "api_type" },
                new object[] { after, before, count.ToString(), limit.ToString(), show, target, "json" });
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

        public dynamic top(string t, string after, string before, int count, int limit, string show, string target, string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/top.json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "t", "after", "before", "count", "limit", "show", "target", "api_type" },
                new object[] { t, after, before, count.ToString(), limit.ToString(), show, target, "json" });
        }

        public dynamic controversial(string t, string after, string before, int count, int limit, string show, string target, string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/controversial.json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "t", "after", "before", "count", "limit", "show", "target", "api_type" },
                new object[] { t, after, before, count.ToString(), limit.ToString(), show, target, "json" });
        }
    }
}
