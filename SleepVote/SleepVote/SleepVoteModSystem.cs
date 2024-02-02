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

        Harmony twlsppatches;

        public override void Start(ICoreAPI api)
        {
            Instance = this;
            twlsppatches = new Harmony("SleepVote.Patches");
        }

        public override void StartServerSide(ICoreServerAPI api)
        {
            ServerApi = api;
            ServerConfig = api.LCConfig<ServerModConfig>(this,false,!api.Server.IsDedicated);
            Commands.Createcmd(api);
            twlsppatches.PatchCategory("Server");
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            ClientAPI = api;
            twlsppatches.PatchCategory("Client");
        }
    }
}
