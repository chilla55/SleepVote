using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleepVote.Extensions;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace SleepVote.server.sleeping
{
    [HarmonyPatch(typeof(ModSleeping), "AreAllPlayersSleeping")]
    public class Patch
    {
        public static int currentlysleeping = 0;
        [HarmonyPrefix]
        public static bool Patch_ModSleeping_AreAllPlayersSleeping_Prefix(ICoreServerAPI ___sapi, ref bool __result)
        {
            if (SleepVote.SleepVoteModSystem.Instance.ServerConfig.DisableSleeping)
                return __result = false;
            List<IServerPlayer> allPlayers = ___sapi.World.AllPlayersThatCouldSleep().ToList();
            if (allPlayers.Count == 0)
                return __result = false;
            int playersSleeping = allPlayers.SleepingPlayers().Count();
            double requiredNumberOfPlayers = Math.Clamp(Math.Floor(allPlayers.Count * SleepVote.SleepVoteModSystem.Instance.ServerConfig.Sleepprecentage), 1, allPlayers.Count);
            __result = playersSleeping >= requiredNumberOfPlayers;
            if (currentlysleeping != playersSleeping)
            {
                currentlysleeping = playersSleeping;
                if (currentlysleeping != 0)
                    SleepVote.server.sleeping.Broadcast.NowSleepingMessage();
            }
            return false;
        }
    }
    [HarmonyPatch(typeof(ModSleeping), "ServerSlowTick")]
    public class Patch2
    {
        internal static double ___lastTickTotalDays = 0;
        [HarmonyPrefix]
        public static bool Patch_ModSleeping_ServerSlowTick_Prefix(ModSleeping __instance,
            ICoreServerAPI ___sapi, IServerNetworkChannel ___serverChannel)
        {
            bool flag = __instance.AreAllPlayersSleeping();

            if (flag == __instance.AllSleeping) return false;
            //if (flag) TWLSP.server.sleeping.Broadcast.NowSleepingMessage();
            ___serverChannel.BroadcastPacket(new NetworksMessageAllSleepMode { On = flag });
            __instance.AllSleeping = flag;
            return false;
        }
    }
}
