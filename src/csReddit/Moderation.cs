using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csReddit
{
    public class Moderation
    {
        public string error;
        public string warning;

        private Account Account;
        public Moderation(Account Account)
        {
            this.Account = Account;
        }

        public Moderation() : this(null) { }

        public dynamic about_log(string after, string before, int count, string show, string target, 
            string type, string subreddit = "", int limit = 25, string mod = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/about/log.json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "after", "before", "count", "show", "target", "type", "limit", "mod" },
                new object[] { after, before, count.ToString(), show, target, type, limit.ToString(), mod });
        }

        public bool accept_moderator_invite(string subreddit = "")
        {
            return (API.Retrieve((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/accept_moderator_invite", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "api_type" },
                new object[] { "json" }) != "");
        }

        public bool approve(string id)
        {
            return (API.Retrieve(@"/api/approve", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id" },
                new object[] { id }) != "");
        }

        public bool distinguish(string how, string id)
        {
            return (API.Retrieve(@"/api/distinguish", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "how", "id", "api_type" },
                new object[] { how, id, "json" }) != "");
        }

        public bool ignore_reports(string id)
        {
            return (API.Retrieve(@"/api/ignore_reports", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id", "api_type" },
                new object[] { id, "json" }) != "");
        }

        public bool leavecontributor(string id)
        {
            return (API.Retrieve(@"/api/leavecontributor", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id", "api_type" },
                new object[] { id, "json" }) != "");
        }

        public bool leavemoderator(string id)
        {
            return (API.Retrieve(@"/api/leavemoderator", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
               Account, new List<string> { "id", "api_type" },
               new object[] { id, "json" }) != "");
        }

        public bool remove(string id, bool spam = false)
        {
            return (API.Retrieve(@"/api/remove", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id", "spam", "api_type" },
                new object[] { id, spam.ToString(), "json" }) != "");
        }

        public bool unignore_reports(string id)
        {
            return (API.Retrieve(@"/api/unignore_reports", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id", "api_type" },
                new object[] { id, "json" }) != "");
        }

        public dynamic stylesheet(string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/stylesheet", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { },
                new object[] { });
        }
    }
}
