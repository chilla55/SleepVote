using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace SleepVote.client.Sleeping
{
    internal class Patches
    {
        public sealed class ModSleepingClientPatches
        {
            [HarmonyPrefix]
            [HarmonyPatch(typeof(ModSleeping), "WakeAllPlayers")]
            public static bool Patch_ModSleeping_WakeAllPlayers_Prefix(ModSleeping __instance, ICoreClientAPI ___capi)
            {
                __instance.GameSpeedBoost = 0f;
                ___capi.World.Calendar.SetTimeSpeedModifier("sleeping", __instance.GameSpeedBoost);
                var player = ___capi.World.Player;
                if (player.Entity?.MountedOn is BlockEntityBed) player.Entity.TryUnmount();
                __instance.AllSleeping = false;
                return false;
            }
        }
    }
}
