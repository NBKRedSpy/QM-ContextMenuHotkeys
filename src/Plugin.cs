using HarmonyLib;
using MGSC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace QM_ContextMenuHotkeys
{
    public static class Plugin
    {
        public static ModConfig Config { get; set; }

        public static string ModAssemblyName { get; private set; }

        /// <summary>
        /// The full path to the config file.  Stored in the mod's persistence folder.
        /// </summary>
        public static string ConfigPath { get; private set; }

        /// <summary>
        /// This mod's persistence folder.
        /// </summary>
        public static string ModsPersistenceFolder { get; private set; }

        /// <summary>
        /// The Quasimorph_Mods folder that is parallel to the game's folder.
        /// This is a workaround for Quasimorph syncing and overwriting all files in the 
        /// Game's App Data folder.
        /// </summary>
        private static string AllModsConfigFolder { get; set; }

        static Plugin()
        {
            ModAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            AllModsConfigFolder = Path.Combine(Application.persistentDataPath, "../Quasimorph_ModConfigs/");
            ModsPersistenceFolder = Path.Combine(AllModsConfigFolder, ModAssemblyName);
            ConfigPath = Path.Combine(ModsPersistenceFolder, ModAssemblyName + ".json");
        }


        [Hook(ModHookType.AfterBootstrap)]
        public static void Bootstrap(IModContext context)
        {
            Directory.CreateDirectory(AllModsConfigFolder);
            Directory.CreateDirectory(ModsPersistenceFolder);

            UpgradeModDirectory();

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

        /// <summary>
        /// Moves the config files from the legacy directory to the new directory.
        /// </summary>
        private static void UpgradeModDirectory()
        {
            try
            {
                string oldConfigFile = Path.Combine(Application.persistentDataPath, "QM_ContextMenuHotkeys.json");


                if (!File.Exists(oldConfigFile)) return;

                Directory.CreateDirectory(ModsPersistenceFolder);

                Debug.LogWarning($"Moving config folder from '{oldConfigFile}' to '{ModsPersistenceFolder}");
                File.Move(oldConfigFile, ConfigPath);
            }
            catch (Exception ex)
            {
                Debug.Log($"Unable to move the config files.  Exception: {ex.ToString()}");
            }
        }

        private static void LoadConfig()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            };



            if (!File.Exists(ConfigPath))
            {
                CreateDefaultConfig(ConfigPath, settings);
                return;
            }

            try
            {
                Config = JsonConvert.DeserializeObject<ModConfig>(File.ReadAllText(ConfigPath), settings);

                if(ConvertToLatest(Config))
                {
                    //Write the file to have the latest options.
                    Debug.Log($"Upgrading config to version {Config.ConfigVersion}");   
                    File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(Config, settings));
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Unable to process config {ConfigPath}.  Loading defaults");
                Debug.LogError(ex);
                CreateDefaultConfig(ConfigPath, settings);
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
