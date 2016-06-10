using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace csReddit
{
    public class Account
    {
        public string error;
        public string warning;
        public int ratelimit = 0;  // Minutes

        public bool has_mail;
        public string name;
        public int created;
        public string modhash;
        public int created_utc;
        public int link_karma;
        public int comment_karma;
        public bool over_18;
        public bool is_gold;
        public bool is_mod;
        public bool has_verified_email;
        public string id;
        public bool has_mod_mail;

        internal string pass;

        public CookieContainer cookies;
        public Dictionary<string, string> authheaders;

        CookieCollection cookiecollection;

        Dictionary<string, string> validate;

        public Account(bool login = false, string username = "", string password = "")
        {
            error = "";
            warning = "";

            if (login == true)
            {
                Login(username, password);
            }
        }

        public bool CheckValidation(Dictionary<string, string> validate)
        {
            ratelimit = Int32.Parse(validate["ratelimit"]);
            error = validate["error"];

            return (error == "");
        }

        public bool Login(string username, string password)
        {
            if (username.Trim().ToLower().Substring(0, 3) == @"/u/")
            {
                username = username.Trim().Substring(3);
            }

            Dictionary<string, string> ret = REST.POSTC(out cookiecollection, @"http://www.reddit.com/api/login", @"user=" + username + @"&passwd=" + password + @"&api_type=json");
            
            if (ret["StatusCode"] == "200")
            {
                validate = REST.ValidateReturnData(ret, new Dictionary<string, string>() { { "username", username }, { "password", password } }, true);
                if (CheckValidation(validate) == false)
                {
                    return false;
                }

                name = username;
                pass = password;

                cookies = new CookieContainer();
                cookies.Add(cookiecollection);

                authheaders = new Dictionary<string, string>();
                authheaders.Add("X-Modhash", modhash);

                dynamic data = me();

                has_mail = Convert.ToBoolean(data["has_mail"]);
                name = data["name"];
                created = Convert.ToInt32(data["created"]);
                modhash = data["modhash"];
                created_utc = Convert.ToInt32(data["created_utc"]);
                link_karma = Convert.ToInt32(data["link_karma"]);
                comment_karma = Convert.ToInt32(data["comment_karma"]);
                over_18 = Convert.ToBoolean(data["over_18"]);
                is_gold = Convert.ToBoolean(data["is_gold"]);
                is_mod = Convert.ToBoolean(data["is_mod"]);
                has_verified_email = Convert.ToBoolean(data["has_verified_email"]);
                id = data["id"];
                has_mod_mail = Convert.ToBoolean(data["has_mod_mail"]);

                error = "";

                return true;
            }
            else
            {
                error = "ERROR on login:  " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return false;
            }
        }

        public bool clear_sessions(string dest)
        {
            Dictionary<string, string> ret = REST.POSTC(out cookiecollection, @"http://www.reddit.com/api/clear_sessions",
                @"dest=" + dest + @"&curpass=" + pass + @"&api_type=json", cookies, authheaders);

            if (ret["StatusCode"] == "200")
            {
                validate = REST.ValidateReturnData(ret);
                if (CheckValidation(validate) == false)
                {
                    return false;
                }

                cookies = new CookieContainer();
                cookies.Add(cookiecollection);

                return true;
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return false;
            }
        }

        public bool delete_user(bool confirm, string delete_message, string user = "", string passwd = "")
        {
            user = (user == "" ? name : user);
            passwd = (passwd == "" ? passwd : pass);

            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/delete_user",
                @"confirm=" + confirm.ToString() + @"&delete_message=" + delete_message + @"&passwd=" + passwd + @"user=" + user
                + @"&api_type=json", cookies, authheaders);

            if (ret["StatusCode"] == "200")
            {
                return CheckValidation(REST.ValidateReturnData(ret));
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return false;
            }
        }

        public dynamic me()
        {
            Dictionary<string, string> ret = REST.GET(@"http://www.reddit.com/api/me.json", "", cookies, authheaders);

            if (ret["StatusCode"] != "200")
            {
                error = "ERROR on post-login modhash retrieval : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return new Dictionary<string,string>();
            }
            else if (ret["Body"].Contains("data") == false)
            {
                error = "Reddit API failed to return me.json data after successful login!";

                return new Dictionary<string,string>();
            }
            else
            {
                validate = REST.ValidateReturnData(ret);
                if (CheckValidation(validate) == false)
                {
                    return new Dictionary<string,string>();
                }
            }

            return REST.json_decode(REST.json_prepare(ret["Body"]));
        }

        public bool register(string captcha, string iden, string passwd, bool rem, string user, string email = "")
        {
            Dictionary<string, string> ret = REST.POSTC(out cookiecollection, @"http://www.reddit.com/api/register",
                @"captcha=" + captcha + @"&email=" + email + @"&iden=" + iden + @"&passwd=" + passwd + @"&passwd2=" + passwd
                + @"&rem=" + rem.ToString() + @"&user=" + user
                + @"&api_type=json", cookies, authheaders);

            if (ret["StatusCode"] == "200")
            {
                validate = REST.ValidateReturnData(ret);
                if (CheckValidation(validate) == false)
                {
                    return false;
                }

                cookies = new CookieContainer();
                cookies.Add(cookiecollection);

                return true;
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return false;
            }
        }

        public bool update(string email, string newpass, bool verify)
        {
            Dictionary<string, string> ret = REST.POST(@"http://www.reddit.com/api/update",
                @"curpass=" + pass + @"&email=" + email + @"&newpass=" + newpass + @"&verify=" + verify.ToString() + @"&verpass=" + newpass
                + @"&api_type=json", cookies, authheaders);

            if (ret["StatusCode"] == "200")
            {
                if (CheckValidation(REST.ValidateReturnData(ret)) == true)
                {
                    pass = newpass;

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                error = "ERROR in " + System.Reflection.MethodBase.GetCurrentMethod().Name + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return false;
            }
        }
    }
}
