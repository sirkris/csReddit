using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace csReddit
{
    public static class REST
    {
        public static HttpWebResponse Query(string method, string URL, string Params = "", CookieContainer cookies = default(CookieContainer), Dictionary<string, string> headers = default(Dictionary<string, string>), Dictionary<string, string> files = default(Dictionary<string, string>))
        {
            switch (method.ToUpper().Trim())
            {
                default:
                    break;
                case "GET":
                    if (Params.Length > 0)
                    {
                        URL += @"?" + Params;
                    }
                    break;
            }

            WebRequest request = WebRequest.Create(URL);

            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = method;

            if (cookies == default(CookieContainer))
            {
                ((HttpWebRequest)request).CookieContainer = new CookieContainer();
            }
            else
            {
                ((HttpWebRequest)request).CookieContainer = cookies;
            }

            if (headers == default(Dictionary<string, string>))
            {
                headers = new Dictionary<string, string>();
            }

            if (headers.Count > 0)
            {
                foreach (KeyValuePair<string, string> pair in headers)
                {
                    /* Stupid .NET "reserves" certain headers to other methods and will throw an exception if set via Add().  --Kris */
                    switch (pair.Key.ToLower().Trim())
                    {
                        default:
                            request.Headers.Add(pair.Key, pair.Value);
                            break;
                        case "accept":
                            ((HttpWebRequest)request).Accept = pair.Value;
                            break;
                        case "connection":
                            ((HttpWebRequest)request).Connection = pair.Value;
                            break;
                        case "content-length":
                        case "contentlength":
                            request.ContentLength = Convert.ToInt32(pair.Value);
                            break;
                        case "content-type":
                        case "contenttype":
                            request.ContentType = pair.Value;
                            break;
                        case "date":
                            ((HttpWebRequest)request).Date = DateTime.Parse(pair.Value);
                            break;
                        case "expect":
                            ((HttpWebRequest)request).Expect = pair.Value;
                            break;
                        case "host":
                            ((HttpWebRequest)request).Host = pair.Value;
                            break;
                        case "if-modified-since":
                        case "ifmodifiedsince":
                            ((HttpWebRequest)request).IfModifiedSince = DateTime.Parse(pair.Value);
                            break;
                        case "referer":
                            ((HttpWebRequest)request).Referer = pair.Value;
                            break;
                        case "transfer-encoding":
                        case "transferencoding":
                            ((HttpWebRequest)request).TransferEncoding = pair.Value;
                            break;
                        case "user-agent":
                        case "useragent":
                            ((HttpWebRequest)request).UserAgent = pair.Value;
                            break;
                    }
                }
            }
            
            if (headers.ContainsKey("user-agent") == false && headers.ContainsKey( "useragent" ) == false)
            {
                ((HttpWebRequest)request).UserAgent = "csReddit v" + Reddit.Version + " API Library for C#.  Written by Kris Craig.";
            }

            switch (method.ToUpper().Trim())
            {
                default:
                    break;
                case "DELETE":
                case "POST":
                case "PUT":
                    if (files == default(Dictionary<string, string>) || files.Count == 0)
                    {
                        byte[] datastream = Encoding.UTF8.GetBytes(Params);

                        request.ContentType = "application/x-www-form-urlencoded";
                        request.ContentLength = datastream.Length;

                        Stream stream = request.GetRequestStream();
                        stream.Write(datastream, 0, datastream.Length);
                        stream.Close();
                    }
                    else
                    {
                        string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");

                        request.ContentType = "multipart/form-data; boundary=" + boundary;

                        MemoryStream postDataStream = new MemoryStream();
                        StreamWriter postDataWriter = new StreamWriter(postDataStream);

                        if (Params != "")
                        {
                            foreach (string param in Regex.Split(Params, @"\&"))
                            {
                                string[] pair = Regex.Split(param, @"\=");

                                postDataWriter.Write("\r\n" + boundary + "\r\n");
                                postDataWriter.Write("Content-Disposition: form-data;"
                                    + "name=\"{0}\"\r\n\r\n{1}",
                                    pair[0],
                                    pair[1]);
                            }

                            postDataWriter.Flush();
                        }

                        foreach (KeyValuePair<string, string> file in files)
                        {
                            postDataWriter.Write("\r\n" + boundary + "\r\n");
                            postDataWriter.Write("Content-Disposition: form-data;"
                                + "name=\"{0}\";"
                                + "filename=\"{1}\""
                                + "\r\nContent-Type: {2}\r\n\r\n",
                                file.Key,
                                Path.GetFileName(file.Value),
                                Path.GetExtension(file.Value));

                            postDataWriter.Flush();

                            FileStream fileStream = new FileStream(file.Value, FileMode.Open, FileAccess.Read);
                            byte[] buffer = new byte[1024];
                            int bytesRead = 0;
                            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                            {
                                postDataStream.Write(buffer, 0, bytesRead);
                            }
                            fileStream.Close();
                        }

                        postDataWriter.Write("\r\n" + boundary + "\r\n");
                        postDataWriter.Flush();

                        request.ContentLength = postDataStream.Length;

                        using (Stream s = request.GetRequestStream())
                        {
                            postDataStream.WriteTo(s);
                        }

                        postDataStream.Close();
                   }

                    break;
            }

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException we)
            {
                response = (HttpWebResponse)we.Response;
            }
            
            return response;
        }

        public static Dictionary<string, string> ProcessQueryResponse(HttpWebResponse response)
        {
            /* We really only need the body and *maybe* the status, but this will allow for easier debugging without returning the entire raw response object.  --Kris */
            Dictionary<string, string> ret = new Dictionary<string, string>();

            ret["Status"] = response.StatusCode.ToString();
            ret["StatusCode"] = ((int)response.StatusCode).ToString();
            ret["StatusDescription"] = response.StatusDescription;
            ret["Headers"] = response.Headers.ToString();

            Stream outstream = response.GetResponseStream();
            StreamReader reader = new StreamReader(outstream);
            ret["Body"] = reader.ReadToEnd();
            reader.Close();
            outstream.Close();

            response.Close();

            return ret;
        }

        /* Overload that saves cookies.  --Kris */
        public static Dictionary<string, string> ProcessQueryResponse(HttpWebResponse response, out CookieCollection cookiecollection)
        {
            /* We really only need the body and *maybe* the status, but this will allow for easier debugging without returning the entire raw response object.  --Kris */
            Dictionary<string, string> ret = new Dictionary<string, string>();

            ret["Status"] = response.StatusCode.ToString();
            ret["StatusCode"] = ((int)response.StatusCode).ToString();
            ret["StatusDescription"] = response.StatusDescription;
            ret["Headers"] = response.Headers.ToString();

            Stream outstream = response.GetResponseStream();
            StreamReader reader = new StreamReader(outstream);
            ret["Body"] = reader.ReadToEnd();
            reader.Close();
            outstream.Close();

            cookiecollection = response.Cookies;

            response.Close();

            return ret;
        }

        public static string exP(string p, Dictionary<string, string> exP)
        {
            return (exP.ContainsKey(p) ? exP[p] : "");
        }

        public static Dictionary<string, string> ValidateReturnData(Dictionary<string, string> ret, Dictionary<string, string> exPs = default(Dictionary<string, string>), bool strict = false)
        {
            Dictionary<string, string> validate = new Dictionary<string, string>();
            int ratelimit = 0;

            validate.Add("error", "");
            validate.Add("ratelimit", "0");

            if (ret["Body"].Contains("data") == false)
            {
                if (ret["Body"].Contains("errors") == true)
                {
                    /* JSON returned is jagged.  Short of writing a bunch of custom interfaces (maybe TODO), we'll just do an ifblock for now.  --Kris */
                    if (ret["Body"].Contains("WRONG_PASSWORD") == true)
                    {
                        validate["error"] = "Invalid username and/or password for user '" + exP("username", exPs) + "', pass '" + exP("password", exPs) + "'!";
                    }
                    else if (ret["Body"].Contains("RATELIMIT") == true)
                    {
                        ratelimit = 0;
                        validate["error"] = "";
                        if (ret["Body"].Contains("try again in") == true)
                        {
                            Match m = Regex.Match(ret["Body"], @"try again in (\d+) (\w)");

                            if (m.Groups.Count == 3)
                            {
                                try
                                {
                                    ratelimit = Int32.Parse(m.Groups[1].Value);

                                    switch (m.Groups[2].Value.Trim().ToLower())
                                    {
                                        default:
                                        case "minute":
                                        case "minutes":
                                            break;
                                        case "millisecond":
                                        case "milliseconds":
                                            ratelimit = 1;
                                            break;
                                        case "second":
                                        case "seconds":
                                            ratelimit = (int)Math.Round((double)(ratelimit / 60));
                                            break;
                                        case "hour":
                                        case "hours":
                                            ratelimit *= 60;
                                            break;
                                        case "day":
                                        case "days":
                                            ratelimit *= 60 * 24;
                                            break;
                                    }

                                    validate["error"] = "Reddit API returned RATELIMIT of " + ratelimit.ToString() + (ratelimit == 1 ? " minute" : " minutes") + ".";
                                    ratelimit += 1;
                                    validate["error"] += Environment.NewLine + "Waiting " + ratelimit.ToString() + (ratelimit == 1 ? " minute" : " minutes") + ".";
                                }
                                catch (Exception e)
                                {
                                    validate["error"] = "Exception encountered attempting to extract RATELIMIT : " + e.Message + Environment.NewLine;
                                }
                            }
                            else
                            {
                                validate["error"] = "Regex match error.  Groups count " + m.Groups.Count.ToString() + " != 3." + Environment.NewLine;
                            }
                        }

                        if (ratelimit <= 0)
                        {
                            validate["error"] += "Unknown RATELIMIT returned : " + ret["Body"].Substring(ret["Body"].IndexOf("\"errors\": ") + 10,
                            ret["Body"].IndexOf(@"}") - (ret["Body"].IndexOf("\"errors\": ") + 10)) + Environment.NewLine + "Waiting 5 minutes.";

                            ratelimit = 5;
                        }
                    }
                    else
                    {
                        validate["error"] = "The Reddit API returned an error : " +
                            ret["Body"].Substring(ret["Body"].IndexOf("\"errors\": ") + 10,
                            ret["Body"].IndexOf(@"}") - (ret["Body"].IndexOf("\"errors\": ") + 10));
                    }
                }
                else if (strict == true)
                {
                    validate["error"] = "Login failure!";
                }

                return validate;
            }

            validate["ratelimit"] = ratelimit.ToString();

            return validate;
        }

        public static Dictionary<string, string> GET(string URL, string Params = "", CookieContainer cookies = default(CookieContainer), 
            Dictionary<string, string> headers = default(Dictionary<string, string>))
        {
            return ProcessQueryResponse(Query("GET", URL, Params, cookies, headers));
        }

        public static Dictionary<string, string> GETC(out CookieCollection cookiecollection, string URL, string Params = "", CookieContainer cookies = default(CookieContainer),
            Dictionary<string, string> headers = default(Dictionary<string, string>))
        {
            return ProcessQueryResponse(Query("GET", URL, Params, cookies, headers), out cookiecollection);
        }

        public static Dictionary<string, string> POST(string URL, string Params = "", CookieContainer cookies = default(CookieContainer),
            Dictionary<string, string> headers = default(Dictionary<string, string>), Dictionary<string, string> files = default(Dictionary<string, string>))
        {
            return ProcessQueryResponse(Query("POST", URL, Params, cookies, headers, files));
        }

        public static Dictionary<string, string> POSTC(out CookieCollection cookiecollection, string URL, string Params = "", CookieContainer cookies = default(CookieContainer),
            Dictionary<string, string> headers = default(Dictionary<string, string>), Dictionary<string, string> files = default(Dictionary<string, string>))
        {
            return ProcessQueryResponse(Query("POST", URL, Params, cookies, headers, files), out cookiecollection);
        }

        public static Dictionary<string, string> PUT(string URL, string Params = "", CookieContainer cookies = default(CookieContainer),
            Dictionary<string, string> headers = default(Dictionary<string, string>), Dictionary<string, string> files = default(Dictionary<string, string>))
        {
            return ProcessQueryResponse(Query("PUT", URL, Params, cookies, headers, files));
        }

        public static Dictionary<string, string> PUTC(out CookieCollection cookiecollection, string URL, string Params = "", CookieContainer cookies = default(CookieContainer),
            Dictionary<string, string> headers = default(Dictionary<string, string>), Dictionary<string, string> files = default(Dictionary<string, string>))
        {
            return ProcessQueryResponse(Query("PUT", URL, Params, cookies, headers, files), out cookiecollection);
        }

        public static Dictionary<string, string> DELETE(string URL, string Params = "", CookieContainer cookies = default(CookieContainer),
            Dictionary<string, string> headers = default(Dictionary<string, string>), Dictionary<string, string> files = default(Dictionary<string, string>))
        {
            return ProcessQueryResponse(Query("DELETE", URL, Params, cookies, headers, files));
        }

        public static Dictionary<string, string> DELETEC(out CookieCollection cookiecollection, string URL, string Params = "", CookieContainer cookies = default(CookieContainer),
            Dictionary<string, string> headers = default(Dictionary<string, string>), Dictionary<string, string> files = default(Dictionary<string, string>))
        {
            return ProcessQueryResponse(Query("DELETE", URL, Params, cookies, headers, files), out cookiecollection);
        }

        public static Dictionary<string, string> json_decode(string json)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public static string json_encode(Dictionary<string, string> data)
        {
            return JsonConvert.SerializeObject(data);
        }

        /* Clean-up the JSON string before parsing.  Avoids a lot of messy issues and we're only interested in the "data" contents, anyway.  --Kris */
        public static string json_prepare(string json)
        {
            // Basically just grabs the inner-most (last) {} section of the JSON since recursive parsing isn't natively supported.  TODO?  --Kris
            return json.Substring(json.LastIndexOf(@"{"), json.Substring(json.LastIndexOf(@"{")).Length - 1);
        }

        public static bool is_json(string body)
        {
            return ((body.IndexOf(@"{") == 0 || body.IndexOf(@"[") == 0)
                && char_count(body, '{') == char_count(body, '}')
                && char_count(body, '[') == char_count(body, ']'));
        }

        /* Bulkier code but performs considerably better than one-liner splits/etc.  --Kris */
        public static int char_count(string s, char c)
        {
            int i = 0;
            foreach (char sc in s)
            {
                if (sc == c)
                {
                    i++;
                }
            }

            return i;
        }
    }
}
