using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csReddit
{
    public class Wiki
    {
        public string error;
        public string warning;

        private Account Account;
        public Wiki(Account Account)
        {
            this.Account = Account;
        }

        public Dictionary<string, string> alloweditor(string act, string page, string username, string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/wiki/alloweditor/" + act, "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "page", "username" },
                new object[] { page, username });
        }

        public Dictionary<string, string> edit(string content, string page, string previous, string reason, string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/wiki/edit", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "content", "page", "previous", "reason" },
                new object[] { content, page, previous, reason });
        }

        public Dictionary<string, string> hide(string page, string revision, string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/wiki/hide", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "page", "revision" },
                new object[] { page, revision });
        }

        public Dictionary<string, string> revert(string page, string revision, string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/wiki/revert", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "page", "revision" },
                new object[] { page, revision });
        }

        public Wiki() : this(null) { }
    }
}
