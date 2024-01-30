using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;
using Vintagestory.API.Config;

namespace SleepVote.Shared.utils
{
    internal class ModLang
    {
        public static string GetSuffix(string id) => "sleepvote:" + id;
        public static string Get(string id, params object[] args)
        {
            return Lang.Get("sleepvote:" + id, args);
        }
        public static string GetL(string id, string key, params object[] args)
        {
            string ret = Lang.GetL(key, "sleepvote:" + id, args);
            if (ret == "sleepvote:" + id)
                ret = Lang.GetL("en", "sleepvote:" + id, args);
            return ret;
        }
    }
}
