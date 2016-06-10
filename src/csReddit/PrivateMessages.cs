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
            return (API.Retrieve(@"/api/block", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id", "api_type" },
                new object[] { id, "json" }) != "");
        }

        public bool compose(string captcha, string iden, string subject, string text, string to)
        {
            return (API.Retrieve(@"/api/compose", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "captcha", "iden", "subject", "text", "to", "api_type" },
                new object[] { captcha, iden, subject, text, to, "json" }) != "");
        }

        public bool read_message(string id)
        {
            return (API.Retrieve(@"/api/read_message", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id", "api_type" },
                new object[] { id, "json" }) != "");
        }

        public bool unread_message(string id)
        {
            return (API.Retrieve(@"/api/unread_message", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "id", "api_type" },
                new object[] { id, "json" }) != "");
        }

        public dynamic message(string where, bool mark, string mid, string after, string before, int count, 
            string show, string target, int limit = 25)
        {
            return API.Retrieve_JSON(@"/api/message/" + where + @".json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "mark", "mid", "after", "before", "count", "show", "target", "limit" },
                new object[] { mark.ToString(), mid, after, before, count.ToString(), show, target, limit.ToString() });
        }
    }
}
