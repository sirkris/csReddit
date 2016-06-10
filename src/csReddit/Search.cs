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

        public dynamic search(string after, string before, string q, bool restrict_sr, string sort, 
            string syntax, string t, string subreddit = "", int count = 0, int limit = 25, string show = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "") + @"/search.json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "after", "before", "q", "restrict_sr", "sort", "syntax", "t", "count", "limit", "show" },
                new object[] { after, before, q, Convert.ToBoolean(restrict_sr), sort, syntax, t, count.ToString(), limit.ToString(), show });
        }

        public Search() : this(null) { }
    }
}
