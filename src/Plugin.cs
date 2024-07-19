using HarmonyLib;
using MGSC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace QM_ContextMenuHotkeys
{
    public static class Plugin
    {
        public static ModConfig Config { get; set; }

        [Hook(ModHookType.AfterBootstrap)]
        public static void Bootstrap(IModContext context)
        {
            LoadConfig();
            Config.InitKeyStrings();
            HotkeyMod.Init(Config);


            Harmony harmony = new Harmony("nbk_redspy.QM_ContextMenuHotkeys");

            if(!Config.DisableKeyLock)
            {
                InputHelperPatches.Patch(harmony);
            }
            
            harmony.Patch(AccessTools.Method(typeof(MGSC.ContextMenu), nameof(MGSC.ContextMenu.InitCommands)),
                null, new HarmonyMethod(typeof(ContextMenu_InitCommands_Patch), nameof(ContextMenu_InitCommands_Patch.Postfix))
                );

            harmony.Patch(
                AccessTools.Method(typeof(MGSC.NoPlayerContextMenu), nameof(MGSC.NoPlayerContextMenu.InitCommands)),
                null, new HarmonyMethod(typeof(ContextMenu_InitCommands_Patch), nameof(ContextMenu_InitCommands_Patch.Postfix))
                );
        }

        private static void LoadConfig()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };


            string configPath = Path.Combine(Application.persistentDataPath, "QM_ContextMenuHotkeys.json");

            if (!File.Exists(configPath))
            {
                CreateDefaultConfig(configPath, settings);
                return;
            }

            try
            {
                Config = JsonConvert.DeserializeObject<ModConfig>(File.ReadAllText(configPath), settings);

                if(ConvertToLatest(Config))
                {
                    //Write the file to have the latest options.
                    Debug.Log($"Upgrading config to version {Config.ConfigVersion}");   
                    File.WriteAllText(configPath, JsonConvert.SerializeObject(Config, settings));
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Unable to process config {configPath}.  Loading defaults");
                Debug.LogError(ex);
                CreateDefaultConfig(configPath, settings);
            }
        }


        /// <summary>
        /// If the config is an older version, convert to the latest.
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static bool ConvertToLatest(ModConfig current)
        {

            if (current.ConfigVersion == ModConfig.LatestConfigVersion) return false;


            List<CommandBindKey> defaults = new ModConfig().CommandBinds;
            List<CommandBindKey> existing = current.CommandBinds;

            //Remove already bound commands.
            List<CommandBindKey> newItems = defaults
                .Where(x => !existing.Any(c => c.Command == x.Command)).ToList();

            //Any new command that conflicts with a currently bound key, 
            //  change to not be bound.
            newItems.Where(x => existing.Any(c => c.Key == x.Key))
                .ToList().ForEach(x => x.Key = KeyCode.None);

            existing.AddRange(newItems);

            current.CommandBinds = existing
                .OrderBy(x => x.Key == KeyCode.None)
                .ThenBy(x => x.Key.ToString())
                .ThenBy(x => x.Command.ToString())
                .ToList();

            current.ConfigVersion = ModConfig.LatestConfigVersion;

            return true;
        }

        private static void CreateDefaultConfig(string configPath, JsonSerializerSettings settings)
        {
            Config = new ModConfig();
            Config.ConfigVersion = ModConfig.LatestConfigVersion;

            File.WriteAllText(configPath, JsonConvert.SerializeObject(Config, settings));
        }
    }
}
