using Cairo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleepVote.Shared.utils.commands;
using Vintagestory.API.Common;
using Vintagestory.Server;

namespace SleepVote.server.commands
{
    public static class Sleeping
    {
        /*public static TextCommandResult HungerMult(TextCommandCallingArgs args)
        {
            if (args == null) return ModTextCommandResult.Error("null_Arg");
            if (args.ArgCount > 1) return ModTextCommandResult.Error("Too_many_args");
            ServerModConfig serverConfig = SleepVote.SleepVoteModSystem.Instance.ServerConfig;
            if (serverConfig == null) return ModTextCommandResult.Error("Not_Server");
            if (!args.Parsers[0].IsMissing)
            {
                serverConfig.Sleep.Hungermultiplier = (float)args.Parsers[0].GetValue();
                return ModTextCommandResult.Success("cmdHunger_modify", serverConfig.Sleep.Hungermultiplier);
            }
            return ModTextCommandResult.Success("cmdHunger_show", serverConfig.Sleep.Hungermultiplier);
        }*/
        public static TextCommandResult SleepPrecentage(TextCommandCallingArgs args)
        {
            
            if (args == null) return ModTextCommandResult.Error("null_Arg", args.Caller.Player);
            if (args.ArgCount > 1) return ModTextCommandResult.Error("Too_many_args", args.Caller.Player);
            ServerModConfig serverConfig = SleepVote.SleepVoteModSystem.Instance.ServerConfig;
            if (serverConfig == null) return ModTextCommandResult.Error("Not_Server", args.Caller.Player);
            if (!args.Parsers[0].IsMissing)
            {
                serverConfig.Sleepprecentage = Math.Clamp((float)args.Parsers[0].GetValue(),0f,1f);
                return ModTextCommandResult.Success("cmdSleepPrecentage_modify", args.Caller.Player, serverConfig.Sleepprecentage);
            }
            return ModTextCommandResult.Success("cmdSleepPrecentage_show", args.Caller.Player, serverConfig.Sleepprecentage);
        }
    }
}
