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
            return API.Retrieve_JSON(@"/api/needs_captcha.json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { }, new object[] { });
        }

        public Dictionary<string, string> new_captcha()
        {
            return API.Retrieve_JSON(@"/api/new_captcha", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "api_type" },
                new object[] { "json" });
        }

        // TODO - Needs testing!  Not sure if the return data is JSON or just a raw image.  --Kris
        public Dictionary<string, string> iden(string iden)
        {
            return API.Retrieve_JSON(@"/api/" + iden, "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { },
                new object[] { });
        }
    }
}
