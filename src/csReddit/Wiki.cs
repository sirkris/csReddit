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
        private API API;

        public Wiki(Account Account)
        {
            this.Account = Account;
            this.API = new API();
        }

        public dynamic alloweditor(string act, string page, string username, string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/wiki/alloweditor/" + act, "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "page", "username" },
                new object[] { page, username });
        }

        public dynamic edit(string content, string page, string previous, string reason, string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/wiki/edit", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "content", "page", "previous", "reason" },
                new object[] { content, page, previous, reason });
        }

        public dynamic hide(string page, string revision, string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/wiki/hide", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "page", "revision" },
                new object[] { page, revision });
        }

        public dynamic revert(string page, string revision, string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/wiki/revert", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "page", "revision" },
                new object[] { page, revision });
        }

        public dynamic pageDiscussions(string page, string after, string before, int count = 0, int limit = 25, string show = "all", string sr_detail = "", string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/wiki/discussions/" + page, "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "after", "before", "count", "limit", "show", "sr_detail" },
                new object[] { after, before, count.ToString(), limit.ToString(), show, sr_detail });
        }

        public dynamic pages(string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/wiki/pages", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { },
                new object[] { });
        }

        public dynamic revisions(string after, string before, int count = 0, int limit = 25, string show = "all", string sr_detail = "", string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/wiki/revisions", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "after", "before", "count", "limit", "show", "sr_detail" },
                new object[] { after, before, count.ToString(), limit.ToString(), show, sr_detail });
        }

        public dynamic pageRevisions(string page, string after, string before, int count = 0, int limit = 25, string show = "all", string sr_detail = "", string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/wiki/revisions/" + page, "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "after", "before", "count", "limit", "show", "sr_detail" },
                new object[] { after, before, count.ToString(), limit.ToString(), show, sr_detail });
        }

        public dynamic getPageSettings(string page, string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/wiki/settings/" + page, "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { },
                new object[] { });
        }

        public dynamic postPageSettings(string page, bool listed, int permlevel, string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/wiki/settings/" + page, "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "listed", "permlevel" },
                new object[] { listed.ToString(), permlevel.ToString() });
        }

        public dynamic page(string page, string v = "", string v2 = "", string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/wiki/" + page, "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "v", "v2" },
                new object[] { v, v2 });
        }

        public Wiki() : this(null) { }
    }
}
