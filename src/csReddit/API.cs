using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace csReddit
{
    public class API
    {
        public string baseurl = @"http://www.reddit.com";
        public string last_error = null;
        public string last_status = null;

        public API() { }

        private string combine_vars(List<string> vars, params object[] vals)
        {
            string restvars = "";
            int i = 0;
            foreach (string var in vars)
            {
                if (vals[i] == null)
                {
                    i++;
                    continue;
                }

                restvars += (restvars != "" ? @"&" : "") + var + @"=";
                switch (vals[i].GetType().ToString())
                {
                    default:
                        throw new System.ArgumentException("Unrecognized variable type on index " + i.ToString() + " : " + vals[i].GetType().ToString());
                    case "System.String":
                        restvars += vals[i];
                        break;
                    case "System.Int16":
                    case "System.Int32":
                    case "System.Int64":
                        restvars += vals[i].ToString();
                        break;
                    case "System.Boolean":
                        restvars += Convert.ToString(vals[i]);
                        break;
                }

                i++;
            }

            return restvars;
        }

        public string Retrieve(string path, string rest_method, string caller_method, Account Account,
            List<string> vars, params object[] vals)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            try
            {
                Type t = Type.GetType("csReddit.REST");
                MethodInfo method = t.GetMethod(rest_method, BindingFlags.Public | BindingFlags.Static);

                ret = (Dictionary<string, string>)method.Invoke(null, new object[] {
                baseurl + @"/" + path, combine_vars( vars, vals ), Account.cookies, Account.authheaders
                });
            }
            catch (Exception e)
            {
                last_error = "ERROR calling REST." + rest_method + " : " + e.Message;
            }

            return Parse_Status(ret, Account, caller_method);
        }

        public string Retrieve(string path, string rest_method, string caller_method, Account Account,
            List<string> vars, Dictionary<string, string> files, params object[] vals)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            try
            {
                Type t = Type.GetType("csReddit.REST");
                MethodInfo method = t.GetMethod(rest_method, BindingFlags.Public | BindingFlags.Static);

                ret = (Dictionary<string, string>)method.Invoke(null, new object[] {
                baseurl + @"/" + path, combine_vars( vars, vals ), Account.cookies, Account.authheaders, files
                });
            }
            catch (Exception e)
            {
                last_error = "ERROR calling REST." + rest_method + " : " + e.Message;
            }

            return Parse_Status(ret, Account, caller_method);
        }

        public string Parse_Status(Dictionary<string, string> ret, Account Account, string caller_method)
        {
            if (ret.ContainsKey("StatusCode") == false)
            {
                last_error = "ERROR in API.Retrieve_JSON from " + caller_method + " : No status code returned!";

                return "";
            }

            last_status = ret["StatusCode"];
            
            if (ret["StatusCode"] == "200")
            {
                if (Account.CheckValidation(REST.ValidateReturnData(ret)) == true)
                {
                    return ret["Body"];
                }
                else
                {
                    return "";
                }
            }
            else
            {
                last_error = "ERROR in " + caller_method + " : " + ret["StatusDescription"] + @" (" + ret["StatusCode"] + @")";

                return "";
            }
        }

        public dynamic Retrieve_JSON(string path, string rest_method, string caller_method, Account Account, 
            List<string> vars, params object[] vals)
        {
            string body = Retrieve(path, rest_method, caller_method, Account, vars, vals);

            if (REST.is_json(body))
            {
                return REST.json_decode(REST.json_prepare(body));
            }
            else
            {
                return new Dictionary<string, string>()
                {
                    { "data", body }
                };
            }
        }

        public bool to_bool(string str)
        {
            bool res;

            if (Boolean.TryParse(str, out res))
            {
                return res;
            }
            else
            {
                return false;
            }
        }
    }
}
