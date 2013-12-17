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

        public Dictionary<string, string> comment(string text, string thing_id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/comment",
                @"text=" + text + @"&thing_id=" + thing_id 
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

        public bool del(string id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/del",
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

        public bool editusertext(string text, string thing_id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/editusertext",
                @"text=" + text + @"&thing_id=" + thing_id
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

        public bool hide(string id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/hide",
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

        public Dictionary<string, string> info(string id, int limit = 25)
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com/api/info.json", 
                @"id=" + id + @"&limit=" + limit.ToString()
                , Account.cookies, Account.authheaders);

            if (ret["StatusCode"] != "200")
            {
                error = "ERROR on post-login modhash retrieval : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return new Dictionary<string, string>();
            }
            else if (ret["Body"].Contains("data") == false)
            {
                error = "Reddit API failed to return me.json data after successful login!";

                return new Dictionary<string, string>();
            }
            else
            {
                if (Account.CheckValidation(REST.ValidateReturnData(ret)) == false)
                {
                    return new Dictionary<string, string>();
                }
            }

            return REST.json_decode(REST.json_prepare(ret["Body"]));
        }

        public bool marknsfw(string id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/marknsfw",
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

        public Dictionary<string, string> morechildren(string children, string link_id, string sort)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/morechildren",
                @"children=" + children + @"&link_id=" + link_id + @"&sort=" + sort 
               + @"&api_type=json", Account.cookies, Account.authheaders);

            if (ret["StatusCode"] != "200")
            {
                error = "ERROR on post-login modhash retrieval : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return new Dictionary<string, string>();
            }
            else if (ret["Body"].Contains("data") == false)
            {
                error = "Reddit API failed to return me.json data after successful login!";

                return new Dictionary<string, string>();
            }
            else
            {
                if (Account.CheckValidation(REST.ValidateReturnData(ret)) == false)
                {
                    return new Dictionary<string, string>();
                }
            }

            return REST.json_decode(REST.json_prepare(ret["Body"]));
        }

        public bool report(string id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/report",
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

        public bool save(string id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/save",
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

        public bool set_contest_mode(string id, bool state)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/set_contest_mode",
                @"id=" + id + @"&state=" + state.ToString()
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

        public bool set_subreddit_sticky(string id, bool state)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/set_subreddit_sticky",
                @"id=" + id + @"&state=" + state.ToString()
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

        public Dictionary<string, string> submit(string captcha, string extension, string iden, string kind, bool resubmit, bool save, 
            bool sendreplies, string sr, string text, string then, string title)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/submit",
                @"captcha=" + captcha + @"&extension=" + extension + @"&iden=" + iden + @"&kind=" + kind + @"&resubmit=" + resubmit.ToString() + @"&save=" + save.ToString() 
                + @"&sendreplies=" + sendreplies.ToString() + @"&sr=" + sr + @"&text=" + text + @"&then=" + then + @"&title=" + title 
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

        public bool unhide(string id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/unhide",
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

        public bool unmarknsfw(string id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/unmarknsfw",
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

        public bool unsave(string id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/unsave",
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
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/vote",
                @"dir=" + dir.ToString() + @"&id=" + id 
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
