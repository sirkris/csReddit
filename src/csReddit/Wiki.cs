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
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "")
                + @"/api/wiki/alloweditor/" + act,
                @"page=" + page + @"&username=" + username,
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

        public Dictionary<string, string> edit(string content, string page, string previous, string reason, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "")
                + @"/api/wiki/edit",
                @"content=" + content + @"&page=" + page + @"&previous=" + previous + @"&reason=" + reason,
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

        public Dictionary<string, string> hide(string page, string revision, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "")
                + @"/api/wiki/hide",
                @"page=" + page + @"&revision=" + revision,
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

        public Dictionary<string, string> revert(string page, string revision, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "")
                + @"/api/wiki/revert",
                @"page=" + page + @"&revision=" + revision,
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

        public Wiki() : this(null) { }
    }
}
