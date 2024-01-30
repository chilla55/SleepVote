﻿using System.Text.RegularExpressions;
using System;
using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.API.Config;
using Vintagestory;
using Vintagestory.Server;
using SleepVote.Shared.utils;

namespace SleepVote.server.commands
{
    internal class Commands
    {
        public static void Createcmd(ICoreServerAPI api)
        {
            api.ChatCommands.Create()
                .WithName("Sleepvote")
                .RequiresPrivilege(Privilege.root)
                .BeginSubCommand("SleepPrecentage")
                    .WithArgs(api.ChatCommands.Parsers.OptionalFloat("Sleep Precentage", 1f))
                    .HandleWith(Sleeping.SleepPrecentage)
                    .WithDescription(ModLang.GetSuffix("CMDSleepPrecentage"))
                .EndSubCommand();
        }
    }
}