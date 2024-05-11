using Cairo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleepVote.Shared.utils.commands;
using Vintagestory.API.Common;
using Vintagestory.Server;
using Vintagestory.API.Server;

namespace SleepVote.server.commands
{
    public static class Sleeping
    {
        public static TextCommandResult SleepPrecentage(TextCommandCallingArgs args)
        {
            if (args == null) return ModTextCommandResult.Error("null_Arg", args.Caller.Player);
            if (args.ArgCount > 1) return ModTextCommandResult.Error("Too_many_args", args.Caller.Player);
            ServerModConfig serverConfig = SleepVote.SleepVoteModSystem.Instance.ServerConfig;
            if (serverConfig == null) return ModTextCommandResult.Error("Not_Server", args.Caller.Player);
            if (!args.Parsers[0].IsMissing)
            {
                serverConfig.Sleepprecentage = Math.Clamp((float)args.Parsers[0].GetValue(),0f,1f);
                serverConfig.Save(SleepVote.SleepVoteModSystem.Instance.ServerApi);
                return ModTextCommandResult.Success("cmdSleepPrecentage_modify", args.Caller.Player, serverConfig.Sleepprecentage);
            }
            return ModTextCommandResult.Success("cmdSleepPrecentage_show", args.Caller.Player, serverConfig.Sleepprecentage);
        }
        public static TextCommandResult DisableSleeping(TextCommandCallingArgs args)
        {
            ServerModConfig serverConfig = SleepVote.SleepVoteModSystem.Instance.ServerConfig;
            if (serverConfig == null) return ModTextCommandResult.Error("Not_Server", args.Caller.Player);
            if (args.ArgCount > 1) return ModTextCommandResult.Error("Too_many_args", args.Caller.Player);
            if (args.ArgCount == 0)
            {
                serverConfig.DisableSleeping = !serverConfig.DisableSleeping;
                return ModTextCommandResult.Success("cmdDisableSleeping_" + serverConfig.DisableSleeping, args.Caller.Player);
            }
            if (args.Parsers[0].GetValue() is bool value)
            {
                serverConfig.DisableSleeping = value;
                serverConfig.Save(SleepVote.SleepVoteModSystem.Instance.ServerApi);
                return ModTextCommandResult.Success("cmdDisableSleeping_"+value, args.Caller.Player);
            }
            return ModTextCommandResult.Error("NotBool", args.Caller.Player);
        }
    }
}
