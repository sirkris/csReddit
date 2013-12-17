using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace csReddit
{
    public class Reddit
    {
        public static string LibName = "csReddit";
        public static string Version = "1.00";
        public static string Author = "Kris Craig";
        public static string Email = "kriscraig@php.net";
        public static string Repo = "https://github.com/sirkris/csReddit";

        /* Singleton instances of the various API classes.  --Kris */
        public Account Account;
        public Apps Apps;
        public Captcha Captcha;
        public Flair Flair;
        public LinksAndComments LinksAndComments;
        public Listings Listings;
        public PrivateMessages PrivateMessages;
        public Moderation Moderation;
        public Multis Multis;
        public Search Search;
        public Subreddits Subreddits;
        public Users Users;
        public Wiki Wiki;
        
        /*
          * Note on logging:
          * 
          * The csLog library is OPTIONAL and not included in this repo.
          * It is recommended as it enables the INI library to generate useful logs.
          * 
          * If you do NOT want to use the csLog library, pass false for the logging 
          * argument when instantiating the INI class.  If you want to use an existing 
          * instance of the csLog class, simply pass the Assembly, Type, and instance 
          * variables when instantiating the INI class.
          * 
          * If the csLog library is not present or fails to load during instantiation, 
          * a caught Exception will occur.  Because csLog is not critical to the INI 
          * library's successful operation, the default behavior is for the Exception 
          * to be ignored.  You can change this by passing false for the failSilently 
          * argument when instantiating the INI class.  That will cause the Exception 
          * to be thrown normally.
          * 
          * --Kris
          */
        protected const string Logname = "Reddit";
        protected string logLibDir = Environment.CurrentDirectory;

        protected Assembly csLog = null;
        protected Type csLogType = null;
        protected object csLogInstance = null;

        protected bool csLogEnabled = false;

        public Reddit(bool logging = true, bool failSilently = true, Assembly csLogPass = null, Type csLogTypePass = null, object csLogInstancePass = null)
        {
            if (logging == true)
            {
                csLogEnabled = InitLog(failSilently, csLogPass, csLogTypePass, csLogInstancePass);
            }

            InitSingletons();
        }

        public Reddit() : this(true) { }

        internal void InitSingletons()
        {
            Account = new Account();
            Apps = new Apps(Account);
            Captcha = new Captcha(Account);
            Flair = new Flair(Account);
            LinksAndComments = new LinksAndComments(Account);
            Listings = new Listings(Account);
            PrivateMessages = new PrivateMessages(Account);
            Moderation = new Moderation(Account);
            Multis = new Multis(Account);
            Search = new Search(Account);
            Subreddits = new Subreddits(Account);
            Users = new Users(Account);
            Wiki = new Wiki(Account);
        }

        internal bool InitLog(bool failSilently, Assembly csLogPass, Type csLogTypePass, object csLogInstancePass)
        {
            if (csLogPass == null)
            {
                try
                {
                    csLog = Assembly.LoadFile(logLibDir + @"\csLog.dll");
                }
                catch (Exception e)
                {
                    if (failSilently == false)
                    {
                        throw e;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                csLog = csLogPass;
            }

            if (csLog != null
                && csLogTypePass == null)
            {
                try
                {
                    csLogType = csLog.GetType("csLog.Log");
                }
                catch (Exception e)
                {
                    if (failSilently == false)
                    {
                        throw e;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                csLogType = csLogTypePass;
            }

            if (csLog != null && csLogType != null
                && csLogInstancePass == null)
            {
                try
                {
                    csLogInstance = Activator.CreateInstance(csLogType);
                }
                catch (Exception e)
                {
                    if (failSilently == false)
                    {
                        throw e;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                csLogInstance = csLogInstancePass;
            }

            /* Initialize the INI log.  --Kris */
            try
            {
                csLogType.InvokeMember("Init", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, csLogInstance,
                    new object[] { Logname, "string" });
            }
            catch (Exception e)
            {
                if (failSilently == false)
                {
                    throw e;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /* Interact with the log handler.  --Kris */
        internal void Log(string text = null, string action = "append", bool newline = true)
        {
            if (csLogEnabled == true)
            {
                switch (action.ToLower())
                {
                    default:
                    case "append":
                        csLogType.InvokeMember("Append", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public | BindingFlags.OptionalParamBinding,
                            null, csLogInstance, new object[] { Logname, text, newline });
                        break;
                    case "increment":
                        csLogType.InvokeMember("Increment", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public | BindingFlags.OptionalParamBinding,
                            null, csLogInstance, new object[] { Logname, Int32.Parse(text) });
                        break;
                    case "decrement":
                        csLogType.InvokeMember("Decrement", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public | BindingFlags.OptionalParamBinding,
                            null, csLogInstance, new object[] { Logname, Int32.Parse(text) });
                        break;
                    case "save":
                        csLogType.InvokeMember("Save", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, csLogInstance,
                            new object[] { Logname });
                        break;
                }
            }
        }
    }
}
