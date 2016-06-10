using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csReddit
{
    public class LinksAndComments
    {
        public string error;
        public string warning;

        private Account Account;
        public LinksAndComments(Account Account)
        {
            this.Account = Account;
        }

        public LinksAndComments() : this(null) { }

        public dynamic comment(string text, string thing_id)
        {
            return API.Retrieve_JSON(@"/api/comment", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "text", "thing_id", "api_type" },
                new object[] { text, thing_id, "json" });
        }

        public bool del(string id)
        {
            return (API.Retrieve(@"/api/del", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id", "api_type" },
                new object[] { id, "json" }) != "");
        }

        public bool editusertext(string text, string thing_id)
        {
            return (API.Retrieve(@"/api/editusertext", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "text", "thing_id", "api_type" },
                new object[] { text, thing_id, "json" }) != "");
        }

        public bool hide(string id)
        {
            return (API.Retrieve(@"/api/hide", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id", "api_type" },
                new object[] { id, "json" }) != "");
        }

        public dynamic info(string id, int limit = 25)
        {
            return API.Retrieve_JSON(@"/api/info.json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id", "limit", "api_type" },
                new object[] { id, limit, "json" });
        }

        public bool marknsfw(string id)
        {
            return (API.Retrieve(@"/api/marknsfw", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id", "api_type" },
                new object[] { id, "json" }) != "");
        }

        public dynamic morechildren(string children, string link_id, string sort)
        {
            return API.Retrieve_JSON(@"/api/morechildren", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "children", "link_id", "sort", "api_type" },
                new object[] { children, link_id, sort, "json" });
        }

        public bool report(string id)
        {
            return (API.Retrieve(@"/api/report", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id", "api_type" },
                new object[] { id, "json" }) != "");
        }

        public bool save(string id)
        {
            return (API.Retrieve(@"/api/save", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id", "api_type" },
                new object[] { id, "json" }) != "");
        }

        public bool set_contest_mode(string id, bool state)
        {
            return (API.Retrieve(@"/api/set_contest_mode", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id", "state", "api_type" },
                new object[] { id, state.ToString(), "json" }) != "");
        }

        public bool set_subreddit_sticky(string id, bool state)
        {
            return (API.Retrieve(@"/api/set_subreddit_sticky", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id", "state", "api_type" },
                new object[] { id, state.ToString(), "json" }) != "");
        }

        public dynamic submit(string captcha, string extension, string iden, string kind, bool resubmit, bool save, 
            bool sendreplies, string sr, string text, string then, string title)
        {
            return API.Retrieve_JSON(@"/api/submit", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "captcha", "extension", "iden", "kind", "resubmit", "save", "sendreplies", "sr", "text", "then", "title", "api_type" },
                new object[] { captcha, extension, iden, kind, resubmit.ToString(), save.ToString(), sendreplies.ToString(), sr, text, then, title, "json" });
        }

        public bool unhide(string id)
        {
            return (API.Retrieve(@"/api/unhide", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id", "api_type" },
                new object[] { id, "json" }) != "");
        }

        public bool unmarknsfw(string id)
        {
            return (API.Retrieve(@"/api/unmarknsfw", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id", "api_type" },
                new object[] { id, "json" }) != "");
        }

        public bool unsave(string id)
        {
            return (API.Retrieve(@"/api/unsave", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id", "api_type" },
                new object[] { id, "json" }) != "");
        }

        /*
         * WARNING:  Automated voting by bots is a violation of Reddit's rules!
         * 
         * "Note: votes must be cast by humans. That is, API clients proxying a human's 
         * action one-for-one are OK, but bots deciding how to vote on content or amplifying a 
         * human's vote are not. See the reddit rules for more details on what constitutes 
         * vote cheating."
         * 
         * Taken from:  http://www.reddit.com/dev/api#POST_api_vote
         * 
         * --Kris
         */
        public bool vote(int dir, string id)
        {
            return (API.Retrieve(@"/api/vote", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "dir", "id", "api_type" },
                new object[] { dir.ToString(), id, "json" }) != "");
        }
    }
}
