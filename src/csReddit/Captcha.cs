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
        private API API;

        public Captcha(Account Account)
        {
            this.Account = Account;
            this.API = new API();
        }

        public Captcha() : this(null) { }

        public dynamic needs_captcha()
        {
            return API.Retrieve_JSON(@"/api/needs_captcha.json", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { }, new object[] { });
        }

        public dynamic new_captcha()
        {
            return API.Retrieve_JSON(@"/api/new_captcha", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "api_type" },
                new object[] { "json" });
        }

        // TODO - Needs testing!  Not sure if the return data is JSON or just a raw image.  --Kris
        public dynamic iden(string iden)
        {
            return API.Retrieve_JSON(@"/api/" + iden, "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { },
                new object[] { });
        }
    }
}
