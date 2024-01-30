using HarmonyLib;
using ProperVersion;
using System.Reflection;
using System.Transactions;
using SleepVote.Extensions.ModConfig;
using SleepVote.server;
using SleepVote.server.commands;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;

namespace SleepVote
{
    public class SleepVoteModSystem : ModSystem
    {
        public static SleepVoteModSystem Instance;

        public ICoreClientAPI ClientAPI;

        public ICoreServerAPI ServerApi;
        public server.ServerModConfig ServerConfig;

        public override void Start(ICoreAPI api)
        {
            Instance = this;
            Harmony twlsppatches = new Harmony("SleepVote.Patches");
            twlsppatches.PatchAll();
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
            ServerApi = api;
            ServerConfig = api.LCConfig<ServerModConfig>(this);
            Commands.Createcmd(api);
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            ClientAPI = api;
        }
    }
}
