using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csReddit
{
    public class Captcha
    {
        public string error;
        public string warning;

        private Account Account;
        public Captcha(Account Account)
        {
            this.Account = Account;
        }

        public Captcha() : this(null) { }

        public Dictionary<string, string> needs_captcha()
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com/api/needs_captcha.json", "", Account.cookies, Account.authheaders);

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

        public Dictionary<string, string> new_captcha()
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/new_captcha", "api_type=json", Account.cookies, Account.authheaders);

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

        // TODO - Needs testing!  Not sure if the return data is JSON or just a raw image.  --Kris
        public Dictionary<string, string> iden(string iden)
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com/api/" + iden, "", Account.cookies, Account.authheaders);

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
    }
}
