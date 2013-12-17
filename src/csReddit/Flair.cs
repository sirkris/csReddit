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
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "") + @"/api/clearflairtemplates",
                @"flair_type=" + flair_type
                + @"&api_type=json", Account.cookies, Account.authheaders);

            if (ret["StatusCode"] == "200")
            {
                return Account.CheckValidation(REST.ValidateReturnData(ret));
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return false;
            }
        }

        public bool deleteflair(string name, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "") + @"/api/deleteflair",
                @"name=" + name 
                + @"&api_type=json", Account.cookies, Account.authheaders);

            if (ret["StatusCode"] == "200")
            {
                return Account.CheckValidation(REST.ValidateReturnData(ret));
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return false;
            }
        }

        public bool deleteflairtemplate(string flair_template_id, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "") + @"/api/deleteflairtemplate",
                @"flair_template_id=" + flair_template_id
                + @"&api_type=json", Account.cookies, Account.authheaders);

            if (ret["StatusCode"] == "200")
            {
                return Account.CheckValidation(REST.ValidateReturnData(ret));
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return false;
            }
        }

        public bool flair(string css_class, string link, string name, string text, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "") + @"/api/flair",
                @"css_class=" + css_class + @"&link=" + link + @"&name=" + name + @"&text=" + text 
                + @"&api_type=json", Account.cookies, Account.authheaders);

            if (ret["StatusCode"] == "200")
            {
                return Account.CheckValidation(REST.ValidateReturnData(ret));
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return false;
            }
        }

        public bool flairconfig(bool flair_enabled, string flair_position, bool flair_self_assign_enabled, 
            string link_flair_position, bool link_flair_self_assign_enabled, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "") + @"/api/flairconfig",
                @"flair_enabled=" + flair_enabled.ToString() + @"&flair_position=" + flair_position + @"&flair_self_assign_enabled=" + flair_self_assign_enabled.ToString()
                + @"&link_flair_position=" + link_flair_position + @"&link_flair_self_assign_enabled=" + link_flair_self_assign_enabled.ToString() 
                + @"&api_type=json", Account.cookies, Account.authheaders);

            if (ret["StatusCode"] == "200")
            {
                return Account.CheckValidation(REST.ValidateReturnData(ret));
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return false;
            }
        }

        public bool flaircsv(string flair_csv, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "") + @"/api/flaircsv",
                @"flair_csv=" + flair_csv
                + @"&api_type=json", Account.cookies, Account.authheaders);

            if (ret["StatusCode"] == "200")
            {
                return Account.CheckValidation(REST.ValidateReturnData(ret));
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return false;
            }
        }

        public Dictionary<string, string> flairlist(string after, string before, int count, int limit, string name, string show, string target, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "") + @"/api/flairlist",
                @"after=" + after + @"&before=" + before + @"&count=" + count.ToString() + @"&limit=" + limit.ToString() + @"&name=" + name 
                + @"&show=" + show + @"&target=" + target 
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

        public bool flairtemplate(string css_class, string flair_template_id, string flair_type, string text, bool text_editable, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "") + @"/api/flairtemplate",
                @"css_class=" + css_class + @"&flair_template_id=" + flair_template_id + @"&flair_type=" + flair_type
                + @"&text=" + text + @"&text_editable=" + text_editable.ToString()
                + @"&api_type=json", Account.cookies, Account.authheaders);

            if (ret["StatusCode"] == "200")
            {
                return Account.CheckValidation(REST.ValidateReturnData(ret));
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return false;
            }
        }

        public bool selectflair(string flair_template_id, string link, string name, string text, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "") + @"/api/selectflair",
                @"flair_template_id=" + flair_template_id + @"&link=" + link + @"&name=" + name + @"&text=" + text 
                + @"&api_type=json", Account.cookies, Account.authheaders);

            if (ret["StatusCode"] == "200")
            {
                return Account.CheckValidation(REST.ValidateReturnData(ret));
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return false;
            }
        }

        public bool setflairenabled(bool flair_enabled, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "") + @"/api/setflairenabled",
                @"flair_enabled=" + flair_enabled.ToString() 
                + @"&api_type=json", Account.cookies, Account.authheaders);

            if (ret["StatusCode"] == "200")
            {
                return Account.CheckValidation(REST.ValidateReturnData(ret));
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return false;
            }
        }
    }
}
