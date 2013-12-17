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

        public Dictionary<string, string> about_log(string after, string before, int count, string show, string target, 
            string type, string subreddit = "", int limit = 25, string mod = "")
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com" + ( subreddit != "" ? @"/r/" + subreddit : "" ) + "/about/log.json",
                @"after=" + after + @"&before=" + before + @"&count=" + count.ToString() + @"&show=" + show + @"&target=" + target 
                + @"&type=" + type + @"&limit=" + limit.ToString() + @"&mod=" + mod, 
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

        public bool accept_moderator_invite(string subreddit = "")
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + ( subreddit != "" ? @"/r/" + subreddit : "" ) + @"/api/accept_moderator_invite",
                @"api_type=json", Account.cookies, Account.authheaders);

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

        public bool approve(string id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/approve",
                @"id=" + id, Account.cookies, Account.authheaders);

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

        public bool distinguish(string how, string id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/distinguish",
                @"how=" + how + @"&id=" + id 
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

        public bool ignore_reports(string id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/ignore_reports",
                @"id=" + id
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

        public bool leavecontributor(string id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/leavecontributor",
                @"id=" + id
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

        public bool leavemoderator(string id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/leavemoderator",
                @"id=" + id
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

        public bool remove(string id, bool spam = false)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/remove",
                @"id=" + id + @"&spam=" + spam.ToString() 
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

        public bool unignore_reports(string id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/unignore_reports",
                @"id=" + id
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

        public Dictionary<string, string> stylesheet(string subreddit = "")
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "") + "/stylesheet",
                "",
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
    }
}
