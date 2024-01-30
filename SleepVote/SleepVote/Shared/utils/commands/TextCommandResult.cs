using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleepVote.Shared.utils;
using Vintagestory.API.Common;

namespace SleepVote.Shared.utils.commands
{
    internal class ModTextCommandResult
    {
        public static TextCommandResult Error(string msgid, string error = "", params object[] args)
        {
            TextCommandResult t = TextCommandResult.Error(ModLang.GetSuffix(msgid), error);
            t.MessageParams = args;
            return t;
        }
        public static TextCommandResult Success(string msgid, params object[] args)
        {
            TextCommandResult t = TextCommandResult.Success(ModLang.GetSuffix(msgid));
            t.MessageParams = args;
            return t;
        }
    }
}
