using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Vintagestory.API.Common;
namespace SleepVote.Extensions.ModConfig
{
    public static class ApiExtensions
    {
        public static string GetWorldId(this ICoreAPI api) => api?.World?.SavegameIdentifier.ToString();

        public static TModConfig LCConfig<TModConfig>(this ICoreAPI api, object caller, bool required = false, bool pserver = false) where TModConfig : ModConfigBase, new()
        {
            string filename = new TModConfig().ModCode;
            if (string.IsNullOrEmpty(filename))
                filename = ModConfig.ModConfigBase.GetModCode(caller);
            if (pserver)
                filename = filename + "-" + api.GetWorldId();

            return LCConfig<TModConfig>(api, filename, required);
        }

        public static TModConfig LCConfig<TModConfig>(this ICoreAPI api, string filename, bool required) where TModConfig : ModConfigBase, new()
        {
            var modCode = filename.Split('.').First();

            try
            {
                if (filename == "")
                    filename = new TModConfig().ModCode;
                var loadedConfig = api.LoadModConfig<TModConfig>(filename);
                if (loadedConfig != null)
                {
                    return loadedConfig;
                }
            }
            catch (Exception e)
            {
                api.World.Logger.Error($"{modCode}: Failed loading modconfig file at 'ModConfig/{filename}', with an error of '{e}'! Stopping...");
                return null;
            }

            var message = $"{modCode}: non-existant modconfig at 'ModConfig/{filename}', creating default" + (required ? " and disabling mod..." : "...");
            api.World.Logger.Notification(message);

            var newConfig = new TModConfig();
            api.StoreModConfig(newConfig, filename);

            return required ? null : newConfig;
        }

        public static void SConfig<TModConfig>(this ICoreAPI api, TModConfig config, bool perServer = false) where TModConfig : ModConfigBase
        {
            var filename = config.ModCode;
            if (string.IsNullOrEmpty(filename))
            {
                filename = ModConfigBase.GetModCode(config);
            }

            if (perServer)
            {
                filename = filename + "-" + api.GetWorldId();
            }

            if (!filename.EndsWith(".json"))
            {
                filename += ".json";
            }

            api.World.Logger.Notification($"Saving modconfig at 'ModConfig/{filename}'...");

            api.StoreModConfig(config, filename);
        }
    }
}
