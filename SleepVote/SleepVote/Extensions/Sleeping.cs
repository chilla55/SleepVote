using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

namespace SleepVote.Extensions
{
    public static class Sleeping
    {
        public static IEnumerable<IServerPlayer> AllPlayersThatCouldSleep(this IServerWorldAccessor world)
            => world.AllOnlinePlayers
                .Cast<IServerPlayer>()
                .Where(p => p?.ConnectionState == EnumClientState.Playing)
                .Where(p => p.WorldData.CurrentGameMode != EnumGameMode.Spectator);

        public static IEnumerable<IServerPlayer> SleepingPlayers(this IEnumerable<IServerPlayer> players)
            => players.Where(p => p.Entity?.MountedOn is BlockEntityBed);

        public static bool Toggle(this ref bool value) => value = !value;
        public static bool EnsureTrue(this ref bool value) => value = true;
        public static bool EnsureFalse(this ref bool value) => value = false;
    }
}
