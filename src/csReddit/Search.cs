using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csReddit
{
    public class Search
    {
        public string error;
        public string warning;

        private Account Account;
        public Search(Account Account)
        {
            this.Account = Account;
        }

        public Dictionary<string, string> search(string after, string before, string q, bool restrict_sr, string sort, 
            string syntax, string t, string subreddit = "", int count = 0, int limit = 25, string show = "")
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com/" + (subreddit != "" ? @"/r/" + subreddit : "") + @"/search.json",
                @"after=" + after + @"&before=" + before + @"&q=" + q + @"&restrict_sr=" + Convert.ToBoolean(restrict_sr)
                + @"&sort=" + sort + @"&syntax=" + syntax + @"&t=" + t + @"&count=" + count.ToString() + @"&limit=" + limit.ToString()
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

        public Search() : this(null) { }
    }
}
