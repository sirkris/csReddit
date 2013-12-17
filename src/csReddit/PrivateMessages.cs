using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csReddit
{
    public class PrivateMessages
    {
        public string error;
        public string warning;

        private Account Account;
        public PrivateMessages(Account Account)
        {
            this.Account = Account;
        }

        public PrivateMessages() : this(null) { }

        public bool block(string id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/block",
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

        public bool compose(string captcha, string iden, string subject, string text, string to)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/compose",
                @"captcha=" + captcha + @"&iden=" + iden + @"&subject=" + subject + @"&text=" + text + @"&to=" + to 
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

        public bool read_message(string id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/read_message",
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

        public bool unread_message(string id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/unread_message",
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

        public Dictionary<string, string> message(string where, bool mark, string mid, string after, string before, int count, 
            string show, string target, int limit = 25)
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com/message/" + where + @".json",
                @"mark=" + mark.ToString() + @"&mid=" + mid + @"&after=" + after + @"&before=" + before + @"&count=" + count.ToString() 
                + @"&show=" + show  + @"&target=" + target + @"&limit=" + limit.ToString(),
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
