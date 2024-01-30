using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleepVote.Extensions;
using SleepVote.Shared.utils;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.MathTools;

namespace SleepVote.server.sleeping
{
    public class Broadcast
    {

        public static void NowSleepingMessage()
        {
            SleepVoteModSystem SVMS = SleepVoteModSystem.Instance;
            try
            {
                var allPlayers = SVMS.ServerApi.World.AllPlayersThatCouldSleep().ToList();
                var sleepingPlayers = allPlayers.SleepingPlayers().Select(p => p.PlayerName).ToList();
                double requiredNumberOfPlayers = Math.Clamp(Math.Floor(allPlayers.Count * SVMS.ServerConfig.Sleepprecentage), 1, allPlayers.Count);


                foreach (var player in allPlayers)
                {
                    double remain = (requiredNumberOfPlayers - sleepingPlayers.Count);
                    var message = ModLang.GetL("NowSleeping", player.LanguageCode, sleepingPlayers.Count,allPlayers.Count, GameMath.Clamp(requiredNumberOfPlayers - sleepingPlayers.Count, 0, allPlayers.Count));
                    SVMS.ServerApi.SendMessage(player, GlobalConstants.AllChatGroups, message, EnumChatType.Notification);
                }
            }
            catch (ArgumentNullException e)
            {
                SVMS.ServerApi.Logger.Error(e.Message);
                SVMS.ServerApi.Logger.Error(e.StackTrace);
                throw;
            }
        }
    }
}
