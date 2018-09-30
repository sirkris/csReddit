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

        // Note - "description" refers to the sidebar, "public_description" refers to the description.  --Kris
        // See:  https://www.reddit.com/r/redditdev/comments/1k8i2h/change_sidebar_via_api/
        public dynamic site_admin(bool allow_discovery, bool allow_images, bool allow_post_crossposts, bool allow_top, bool allow_videos, bool collapse_deleted_comments,
            int comment_score_hide_mins, string description, bool exclude_banned_modqueue, bool free_form_reports, string header_title, bool hide_ads, string key_color,
            string lang, string link_type, string name, bool over_18, string public_description, bool show_media, bool show_media_preview, string spam_comments,
            string spam_links, string spam_selfposts, bool spoilers_enabled, string sr, string r, string submit_link_label, string submit_text, string submit_text_label,
            string suggested_comment_sort, string theme_sr, bool theme_sr_update, string title, string type, string wikimode, int wiki_edit_age = 0,
            int wiki_edit_karma = 0, string modhash = "")
        {
            return API.Retrieve_JSON(@"/api/site_admin", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "allow_discovery", "allow_images", "allow_post_crossposts", "allow_top", "allow_videos", "collapse_deleted_comments", "comment_score_hide_mins", 
                    "description", "exclude_banned_modqueue", "free_form_reports", "header_title", "hide_ads", "key_color", "lang", "link_type", "name", "over_18", "public_description", 
                    "show_media", "show_media_preview", "spam_comments", "spam_links", "spam_selfposts", "spoilers_enabled", "sr", "r", "submit_link_label", "submit_text", "submit_text_label", 
                    "suggested_comment_sort", "theme_sr", "theme_sr_update", "title", "type", "wikimode", "wiki_edit_age", "wiki_edit_karma", "modhash", "api_type" },
                new object[] { allow_discovery.ToString(), allow_images.ToString(), allow_post_crossposts.ToString(), allow_top.ToString(), 
                    allow_videos.ToString(), collapse_deleted_comments.ToString(), comment_score_hide_mins.ToString(), description, exclude_banned_modqueue.ToString(), 
                    free_form_reports.ToString(), header_title, hide_ads.ToString(), key_color, lang, link_type, name, over_18.ToString(), public_description, 
                    show_media.ToString(), show_media_preview.ToString(), spam_comments, spam_links, spam_selfposts, spoilers_enabled.ToString(), sr, r, 
                    submit_link_label, submit_text, submit_text_label, suggested_comment_sort, theme_sr, theme_sr_update.ToString(), title, type, wikimode, 
                    wiki_edit_age.ToString(), wiki_edit_karma.ToString(), modhash, "json" });
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
