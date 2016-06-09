using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csReddit
{
    public class Flair
    {
        public string error;
        public string warning;

        private Account Account;
        public Flair(Account Account)
        {
            this.Account = Account;
        }

        public Flair() : this(null) { }

        public bool clearflairtemplates(string flair_type, string subreddit = "")
        {
            return (API.Retrieve((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/clearflairtemplates", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "flair_type", "api_type" },
                new object[] { flair_type, "json" }) != "");
        }

        public bool deleteflair(string name, string subreddit = "")
        {
            return (API.Retrieve((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/deleteflair", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "name", "api_type" },
                new object[] { name, "json" }) != "");
        }

        public bool deleteflairtemplate(string flair_template_id, string subreddit = "")
        {
            return (API.Retrieve((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/deleteflairtemplate", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "flair_template_id", "api_type" },
                new object[] { flair_template_id, "json" }) != "");
        }

        public bool flair(string css_class, string link, string name, string text, string subreddit = "")
        {
            return (API.Retrieve((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/flair", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "css_class", "link", "name", "text", "api_type" },
                new object[] { css_class, link, name, text, "json" }) != "");
        }

        public bool flairconfig(bool flair_enabled, string flair_position, bool flair_self_assign_enabled,
            string link_flair_position, bool link_flair_self_assign_enabled, string subreddit = "")
        {
            return (API.Retrieve((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/flairconfig", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "flair_enabled", "flair_position", "flair_self_assign_enabled", "link_flair_position", "link_flair_self_assign_enabled", "api_type" },
                new object[] { flair_enabled.ToString(), flair_position, flair_self_assign_enabled.ToString(), link_flair_position, link_flair_self_assign_enabled.ToString(), "json" }) != "");
        }

        public bool flaircsv(string flair_csv, string subreddit = "")
        {
            return (API.Retrieve((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/flaircsv", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "flair_csv", "api_type" },
                new object[] { flair_csv, "json" }) != "");
        }

        public Dictionary<string, string> flairlist(string after, string before, int count, int limit, string name, string show, string target, string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/flairlist", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "after", "before", "count", "limit", "name", "show", "target", "api_type" },
                new object[] { after, before, count.ToString(), limit.ToString(), name, show, target, "json" });
        }

        public bool flairtemplate(string css_class, string flair_template_id, string flair_type, string text, bool text_editable, string subreddit = "")
        {
            return (API.Retrieve((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/flairtemplate", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "css_class", "flair_template_id", "flair_type", "text", "text_editable", "api_type" },
                new object[] { css_class, flair_template_id, flair_type, text, text_editable.ToString(), "json" }) != "");
        }

        public bool selectflair(string flair_template_id, string link, string name, string text, string subreddit = "")
        {
            return (API.Retrieve((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/selectflair", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "flair_template_id", "link", "name", "text", "api_type" },
                new object[] { flair_template_id, link, name, text, "json" }) != "");
        }

        public bool setflairenabled(bool flair_enabled, string subreddit = "")
        {
            return (API.Retrieve((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/setflairenabled", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "flair_enabled", "api_type" },
                new object[] { flair_enabled.ToString(), "json" }) != "");
        }
    }
}
