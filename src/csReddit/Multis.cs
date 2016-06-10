using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csReddit
{
    public class Multis
    {
        public string error;
        public string warning;

        private Account Account;
        public Multis(Account Account)
        {
            this.Account = Account;
        }

        public dynamic mine()
        {
            return API.Retrieve_JSON(@"/api/multi/mine", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { },
                new object[] { });
        }

        public dynamic delete_multipath(string multipath)
        {
            return API.Retrieve_JSON(@"/api/multi/" + multipath, "DELETE", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { },
                new object[] { });
        }

        public dynamic get_multipath(string multipath)
        {
            return API.Retrieve_JSON(@"/api/multi/" + multipath, "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { },
                new object[] { });
        }

        public dynamic create_multipath(string multipath, string model)
        {
            return API.Retrieve_JSON(@"/api/multi/" + multipath, "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "model" },
                new object[] { model });
        }

        public dynamic update_multipath(string multipath, string model)
        {
            return API.Retrieve_JSON(@"/api/multi/" + multipath, "PUT", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "model" },
                new object[] { model });
        }

        public dynamic copy_multipath(string multipath, string from, string to)
        {
            return API.Retrieve_JSON(@"/api/multi/" + multipath + @"/copy", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "from", "to" },
                new object[] { from, to });
        }

        public dynamic get_description(string multipath)
        {
            return API.Retrieve_JSON(@"/api/multi/" + multipath + @"/description", "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { },
                new object[] { });
        }

        public dynamic change_description(string multipath, string model)
        {
            return API.Retrieve_JSON(@"/api/multi/" + multipath + @"/description", "PUT", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "model" },
                new object[] { model });
        }

        public dynamic delete_sub(string multipath, string srname)
        {
            return API.Retrieve_JSON(@"/api/multi/" + multipath + @"/r/" + srname, "DELETE", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { },
                new object[] { });
        }

        public dynamic get_sub(string multipath, string srname)
        {
            return API.Retrieve_JSON(@"/api/multi/" + multipath + @"/r/" + srname, "GET", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { },
                new object[] { });
        }

        public dynamic add_sub(string multipath, string srname, string model)
        {
            return API.Retrieve_JSON(@"/api/multi/" + multipath + @"/r/" + srname, "PUT", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "model" },
                new object[] { model });
        }

        public dynamic rename_multi(string multipath, string from, string to)
        {
            return API.Retrieve_JSON(@"/api/multi/" + multipath + @"/rename", "POST", System.Reflection.MethodBase.GetCurrentMethod().Name,
                Account, new List<string> { "from", "to" },
                new object[] { from, to });
        }

        public Multis() : this(null) { }
    }
}
