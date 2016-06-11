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
        private API API;

        public Apps(Account Account)
        {
            this.Account = Account;
            this.API = new API();
        }

        public Apps() : this(null) { }

        public bool adddeveloper(string client_id, string name)
        {
            return (API.Retrieve(@"/api/adddeveloper", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "client_id", "name", "api_type" },
                new object[] { client_id, name, "json" }) != "");
        }

        public bool deleteapp(string client_id)
        {
            return (API.Retrieve(@"/api/deleteapp", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "client_id", "api_type" },
                new object[] { client_id, "json" }) != "");
        }

        public bool removedeveloper(string client_id, string name)
        {
            return (API.Retrieve(@"/api/removedeveloper", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "client_id", "name", "api_type" },
                new object[] { client_id, name, "json" }) != "");
        }

        public bool revokeapp(string client_id)
        {
            return (API.Retrieve(@"/api/revokeapp", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "client_id", "api_type" },
                new object[] { client_id, "json" }) != "");
        }

        public bool setappicon(string client_id, string filename)
        {
            return (API.Retrieve(@"/api/setappicon", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "client_id", "api_type" },
                new Dictionary<string, string>() { { "file", filename } },
                new object[] { client_id, "json" }) != "");
        }

        public bool updateapp(string about_url, string icon_url, string name, string redirect_uri)
        {
            return (API.Retrieve(@"/api/updateapp", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "about_url", "icon_url", "name", "redirect_uri", "api_type" },
                new object[] { about_url, icon_url, name, redirect_uri, "json" }) != "");
        }
    }
}
