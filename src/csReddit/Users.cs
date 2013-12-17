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
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/friend", 
                @"container=" + container + @"&note=" + note + @"&permissions=" + permissions + @"&type=" + type
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

        public Dictionary<string, string> setpermissions(string name, string permissions, string type, string subreddit = "")
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com" + (subreddit != "" ? @"/r/" + subreddit : "")
                + @"/api/setpermissions", 
                @"name=" + name + @"&permissions=" + permissions + @"&type=" + type
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

        public Dictionary<string, string> unfriend(string container, string id, string name, string type)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/unfriend",
                @"container=" + container + @"&id=" + id + @"&name=" + name + @"&type=" + type,
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

        public Dictionary<string, string> username_available(string user)
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com/api/username_available.json",
                @"user=" + user,
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

        public Dictionary<string, string> about(string username)
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com/user/" + username + @"/about.json",
                @"",
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

        public Dictionary<string, string> get_user(string username, string where, string show, string sort, string t, 
            string after, string before, int count = 0, int limit = 25)
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com/user/" + username + @"/" + where,
                @"show=" + show + @"&sort=" + sort + @"&t=" + t + @"&username=" + username 
                + @"&after=" + after + @"&before=" + before + @"&count=" + count.ToString() + @"&limit=" + limit.ToString(),
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

        public Users() : this(null) { }
    }
}
