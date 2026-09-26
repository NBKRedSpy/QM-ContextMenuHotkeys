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

        internal static JsonSerializerSettings JsonSettings { get; } = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            ObjectCreationHandling = ObjectCreationHandling.Replace
        };

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

            Config = ModConfig.LoadConfig(ConfigPath);
            HotkeyMod.Init(Config);

            Harmony harmony = new Harmony("nbk_redspy.QM_ContextMenuHotkeys");

            InputHelperPatches.Patch(harmony);

            harmony.PatchAll();
        }


        ///// <summary>
        ///// Loads the config file from disk.  If it doesn't exist, creates a new one with defaults.
        ///// Note that this will also re-write the json text file if the version on disk if
        ///// it doesn't match the existing config settings.  For example, if it is missing a setting
        ///// or has legacy settings that are no longer used.
        ///// </summary>
        //private static void LoadConfig()
        //{

        //    if (!File.Exists(ConfigPath))
        //    {
        //        CreateDefaultConfig(ConfigPath, JsonSettings);
        //        return;
        //    }

        //    try
        //    {
        //        string jsonText = File.ReadAllText(ConfigPath);

        //        Config = JsonConvert.DeserializeObject<ModConfig>(jsonText, JsonSettings);

        //        //if(ConvertToLatest(Config))
        //        //{
        //        //    //Write the file to have the latest options.
        //        //    Debug.Log($"Upgrading config to version {Config.ConfigVersion}");   
        //        //    File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(Config, JsonSettings));
        //        //}

        //        //Re-write the config with any missing defaults and remove any old settings.
        //        //Config.RewriteConfigFileIfDifferent(ConfigPath, jsonText);
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.LogError($"Unable to process config {ConfigPath}.  Loading defaults");
        //        Debug.LogError(ex);
        //        Config = new ModConfig();
        //    }
        //}

        //private static void CreateDefaultConfig(string configPath, JsonSerializerSettings settings)
        //{
        //    Config = new ModConfig();
        //    Config.ConfigVersion = ModConfig.LatestConfigVersion;

        //    File.WriteAllText(configPath, JsonConvert.SerializeObject(Config, settings));
        //}
    }
}
