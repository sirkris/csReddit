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
        public Subreddits(Account Account)
        {
            this.Account = Account;
        }

        public Dictionary<string, string> delete_sr_header(string subreddit = "")
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "")
                + @"/api/delete_sr_header",
                @"api_type=json", Account.cookies, Account.authheaders);

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

        public Dictionary<string, string> delete_sr_img(string img_name, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "")
                + @"/api/delete_sr_img",
                @"img_name=" + img_name
                + @"&api_type=json", Account.cookies, Account.authheaders);

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

        public Dictionary<string, string> recommend(string omit, string srnames)
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com/api/recommend/sr/" + srnames,
                @"omit=" + omit, Account.cookies, Account.authheaders);

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

        public Dictionary<string, string> search_reddit_names(bool include_over_18, string query)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/search_reddit_names.json",
                @"include_over_18=" + Convert.ToBoolean(include_over_18) + @"&query=" + query,
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

        public Dictionary<string, string> site_admin(bool allow_top, bool css_on_cname, string description, bool exclude_banned_modqueue, 
            string header_title, string lang, string link_type, string name, bool over_18, string prev_description_id, 
            string prev_public_description_id, string prev_submit_text_id, string public_description, bool public_traffic, 
            bool show_cname_sidebar, string show_media, string spam_comments, string spam_links, string spam_selfposts, string sr, 
            string submit_link_label, string submit_text, string submit_text_label, string title, string type, string wikimode, 
            int comment_score_hide_mins = 0, int wiki_edit_age = 0, int wiki_edit_karma = 0)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/site_admin",
                @"allow_top=" + Convert.ToBoolean(allow_top) + @"&css_on_cname=" + Convert.ToBoolean(css_on_cname)
                + @"&description=" + description + @"&exclude_banned_modqueue=" + Convert.ToBoolean(exclude_banned_modqueue)
                + @"&header_title=" + header_title + @"&lang=" + lang + @"&link_type=" + link_type + @"&name=" + name
                + @"&over_18=" + Convert.ToBoolean(over_18) + @"&prev_description_id=" + prev_description_id
                + @"&prev_public_description_id=" + prev_public_description_id + @"&prev_submit_text_id=" + prev_submit_text_id
                + @"&public_description=" + public_description + @"&public_traffic=" + Convert.ToBoolean(public_traffic)
                + @"&show_cname_sidebar=" + Convert.ToBoolean(show_cname_sidebar) + @"&show_media=" + show_media
                + @"&spam_comments=" + spam_comments + @"&spam_links=" + spam_links + @"&spam_selfposts=" + spam_selfposts
                + @"&sr=" + sr + @"&submit_link_label=" + submit_link_label + @"&submit_text=" + submit_text
                + @"&submit_text_label=" + submit_text_label + @"&title=" + title + @"&type=" + type + @"&wikimode=" + wikimode
                + @"&comment_score_hide_mins=" + comment_score_hide_mins.ToString() + @"&wiki_edit_age=" + wiki_edit_age.ToString()
                + @"&wiki_edit_karma=" + wiki_edit_karma.ToString()
                + @"&api_type=json",
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

        public Dictionary<string, string> submit_text(string subreddit = "")
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "")
                + @"/api/submit_text.json",
                @"", Account.cookies, Account.authheaders);

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

        public Dictionary<string, string> subreddit_stylesheet(string op, string stylesheet_contents, string subreddit = "", string prevstyle = "")
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "")
                + @"/api/subreddit_stylesheet",
                @"op=" + op + @"&stylesheet_contents=" + stylesheet_contents + @"&prevstyle=" + prevstyle
                + @"&api_type=json", Account.cookies, Account.authheaders);

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

        public Dictionary<string, string> subreddits_by_topic(string query)
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com/api/subreddits_by_topic.json",
                @"query=" + query, Account.cookies, Account.authheaders);

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

        public Dictionary<string, string> subscribe(string action, string sr)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/subscribe",
                @"action=" + action + @"&sr=" + sr, Account.cookies, Account.authheaders);

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

        public Dictionary<string, string> upload_sr_img(string file, int header, string name, string subreddit = "", 
            string img_type = "png", string formid = "")
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "")
                + @"/api/upload_sr_img",
                @"file=" + file + @"&header=" + header.ToString() + @"&name=" + name + @"&img_type=" + img_type + @"&formid=" + formid
                , Account.cookies, Account.authheaders);

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

        public Dictionary<string, string> about(string subreddit)
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com/r/" + subreddit + @"/about.json",
                @"", Account.cookies, Account.authheaders);

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

        public Dictionary<string, string> edit(string subreddit, bool created, string location)
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com/r/" + subreddit + @"/about/edit.json",
                @"created=" + Convert.ToBoolean( created ) + @"&location=" + location, 
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

        public Dictionary<string, string> get_subreddits(string where, string after, string before, int count = 0, int limit = 25,
            string show = "")
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com/subreddits/mine/" + where + @".json",
                @"after=" + after + @"&before=" + before + @"&count=" + count.ToString() + @"&limit=" + limit.ToString()
                + @"&show=" + show,
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

        public Dictionary<string, string> search(string q, string after, string before, int count = 0, int limit = 25,
            string show = "")
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com/subreddits/search.json",
                @"q=" + q + @"&after=" + after + @"&before=" + before + @"&count=" + count.ToString() + @"&limit=" + limit.ToString()
                + @"&show=" + show,
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

        public Dictionary<string, string> get_subreddits_all(string where, string after, string before, int count = 0, int limit = 25,
            string show = "")
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com/subreddits/" + where + @".json",
                @"after=" + after + @"&before=" + before + @"&count=" + count.ToString() + @"&limit=" + limit.ToString()
                + @"&show=" + show,
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

        public Subreddits() : this(null) { }
    }
}
