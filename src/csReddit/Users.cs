using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csReddit
{
    public class Users
    {
        public string error;
        public string warning;

        private Account Account;
        public Users(Account Account)
        {
            this.Account = Account;
        }

        public Dictionary<string, string> friend(string container, string name, string note, string permissions, string type)
        {
            return API.Retrieve_JSON(@"/api/friend", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "container", "note", "permissions", "type", "api_type" },
                new object[] { container, note, permissions, type, "json" });
        }

        public Dictionary<string, string> setpermissions(string name, string permissions, string type, string subreddit = "")
        {
            return API.Retrieve_JSON((subreddit != "" ? @"/r/" + subreddit : "")
                + @"/api/setpermissions", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "name", "permissions", "type", "api_type" },
                new object[] { name, permissions, type, "json" });
        }

        public Dictionary<string, string> unfriend(string container, string id, string name, string type)
        {
            return API.Retrieve_JSON(@"/api/unfriend", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "container", "id", "name", "type" },
                new object[] { container, id, name, type });
        }

        public bool username_available(string user)
        {
            string ret = API.Retrieve(@"/api/username_available.json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "user" }, new object[] { user });

            return API.to_bool(ret);
        }

        public Dictionary<string, string> about(string username)
        {
            return API.Retrieve_JSON(@"/user/" + username + @"/about.json", "GET",
                System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { }, new object[] { });
        }

        public Dictionary<string, string> get_user(string username, string where, string show, string sort, string t, 
            string after, string before, int count = 0, int limit = 25)
        {
            return API.Retrieve_JSON(@"/user/" + username + @"/" + where, "GET",
                System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "show", "sort", "t", "username", "after", "before", "count", "limit" },
                new object[] { show, sort, t, username, after, before, count.ToString(), limit.ToString() });
        }

        public Users() : this(null) { }
    }
}
