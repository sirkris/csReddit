using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csReddit
{
    public class Subreddits
    {
        public string error;
        public string warning;

        private Account Account;
        private API API;

        public Subreddits(Account Account)
        {
            this.Account = Account;
            this.API = new API();
        }

        public dynamic delete_sr_header(string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/delete_sr_header", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "api_type" },
                new object[] { "json" });
        }

        public dynamic delete_sr_img(string img_name, string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/delete_sr_img", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "img_name", "api_type" },
                new object[] { img_name, "json" });
        }

        public dynamic recommend(string omit, string srnames)
        {
            return API.Retrieve_JSON(@"/api/recommend/sr/" + srnames, "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "omit" },
                new object[] { omit });
        }

        public dynamic search_reddit_names(bool include_over_18, string query)
        {
            return API.Retrieve_JSON(@"/api/search_reddit_names.json", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "include_over_18", "query" },
                new object[] { Convert.ToBoolean(include_over_18), query });
        }

        public dynamic site_admin(bool allow_top, bool css_on_cname, string description, bool exclude_banned_modqueue, 
            string header_title, string lang, string link_type, string name, bool over_18, string prev_description_id, 
            string prev_public_description_id, string prev_submit_text_id, string public_description, bool public_traffic, 
            bool show_cname_sidebar, string show_media, string spam_comments, string spam_links, string spam_selfposts, string sr, 
            string submit_link_label, string submit_text, string submit_text_label, string title, string type, string wikimode, 
            int comment_score_hide_mins = 0, int wiki_edit_age = 0, int wiki_edit_karma = 0)
        {
            return API.Retrieve_JSON(@"/api/site_admin", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "allow_top", "css_on_cname", "description", "exclude_banned_modqueue", "header_title", "lang", "link_type", "name", 
                    "over_18", "prev_description_id", "prev_public_description_id", "prev_submit_text_id", "public_description", "public_traffic", "show_cname_sidebar", 
                    "show_media", "spam_comments", "spam_links", "spam_selfposts", "sr", "submit_link_label", "submit_text", "submit_text_label", "title", "type", "wikimode", 
                    "comment_score_hide_mins", "wiki_edit_age", "wiki_edit_karma", "api_type" },
                new object[] { Convert.ToBoolean(allow_top), Convert.ToBoolean(css_on_cname), description, Convert.ToBoolean(exclude_banned_modqueue), header_title, lang, link_type, 
                    name, Convert.ToBoolean(over_18), prev_description_id, prev_public_description_id, prev_submit_text_id, public_description, Convert.ToBoolean(public_traffic), 
                    Convert.ToBoolean(show_cname_sidebar), show_media, spam_comments, spam_links, spam_selfposts, sr, submit_link_label, submit_text, submit_text_label, title, 
                    type, wikimode, comment_score_hide_mins.ToString(), wiki_edit_age.ToString(), wiki_edit_karma.ToString(), "json" });
        }

        public dynamic submit_text(string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/submit_text.json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { },
                new object[] { });
        }

        public dynamic subreddit_stylesheet(string op, string stylesheet_contents, string subreddit = "", string prevstyle = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/subreddit_stylesheet", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "op", "stylesheet_contents", "prevstyle", "api_type" },
                new object[] { op, stylesheet_contents, prevstyle, "json" });
        }

        public dynamic subreddits_by_topic(string query)
        {
            return API.Retrieve_JSON(@"/api/subreddits_by_topic.json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "query" },
                new object[] { query });
        }

        public dynamic subscribe(string action, string sr)
        {
            return API.Retrieve_JSON(@"/api/subscribe", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "action", "sr" },
                new object[] { action, sr });
        }

        public dynamic upload_sr_img(string file, int header, string name, string subreddit = "", 
            string img_type = "png", string formid = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/api/upload_sr_img", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "file", "header", "name", "img_type", "formid" },
                new object[] { file, header.ToString(), name, img_type, formid });
        }

        public dynamic about(string subreddit)
        {
            return API.Retrieve_JSON(@"/r/" + subreddit + @"/about.json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { },
                new object[] { });
        }

        public dynamic edit(string subreddit, bool created, string location)
        {
            return API.Retrieve_JSON(@"/about/edit.json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "created", "location" },
                new object[] { Convert.ToBoolean(created), location });
        }

        public dynamic get_subreddits(string where, string after, string before, int count = 0, int limit = 25,
            string show = "")
        {
            return API.Retrieve_JSON(@"/subreddits/mine/" + where + @".json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "after", "before", "count", "limit", "show" },
                new object[] { after, before, count.ToString(), limit.ToString(), show });
        }

        public dynamic search(string q, string after, string before, int count = 0, int limit = 25,
            string show = "")
        {
            return API.Retrieve_JSON(@"/subreddits/search.json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "q", "after", "before", "count", "limit", "show" },
                new object[] { q, after, before, count.ToString(), limit.ToString(), show });
        }

        public dynamic get_subreddits_all(string where, string after, string before, int count = 0, int limit = 25,
            string show = "")
        {
            return API.Retrieve_JSON(@"/subreddits/" + where + @".json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "after", "before", "count", "limit", "show" },
                new object[] { after, before, count.ToString(), limit.ToString(), show });
        }

        public Subreddits() : this(null) { }
    }
}
