using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleepVote.Shared.utils;
using Vintagestory.API.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SleepVote.Shared.utils.commands
{
    internal class ModTextCommandResult
    {
        public static TextCommandResult Error(string msgid, IPlayer player, string error = "", params object[] args)
        {
            if (player == null)
            {
                return TextCommandResult.Error(ModLang.Get(msgid, args), error);
            }
            TextCommandResult t = TextCommandResult.Error(ModLang.GetSuffix(msgid), error);
            t.MessageParams = args;
            return t;
        }
        public static TextCommandResult Success(string msgid, IPlayer player, params object[] args)
        {
            if (player == null)
            {
                return TextCommandResult.Success(ModLang.Get(msgid, args));
            }
            TextCommandResult t = TextCommandResult.Success(ModLang.GetSuffix(msgid));
            t.MessageParams = args;
            return t;
        }
    }
}
