using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csReddit
{
    public class Apps
    {
        public string error;
        public string warning;

        private Account Account;
        public Apps(Account Account)
        {
            this.Account = Account;
        }

        public Apps() : this(null) { }

        public bool adddeveloper(string client_id, string name)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/adddeveloper",
                @"client_id=" + client_id + @"&name=" + name
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

        public bool deleteapp(string client_id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/deleteapp",
                @"client_id=" + client_id
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

        public bool removedeveloper(string client_id, string name)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/removedeveloper",
                @"client_id=" + client_id + @"&name=" + name
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

        public bool revokeapp(string client_id)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/revokeapp",
                @"client_id=" + client_id
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

        public bool setappicon(string client_id, string filename)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/setappicon",
                @"client_id=" + client_id
                + @"&api_type=json", Account.cookies, Account.authheaders, new Dictionary<string, string>() { { "file", filename } });

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

        public bool updateapp(string about_url, string icon_url, string name, string redirect_uri)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/updateapp",
                @"about_url=" + about_url + @"&icon_url=" + icon_url + @"&name=" + name + @"&redirect_uri=" + redirect_uri 
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
