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


        }
        private static void LoadConfig()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
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
            }
            catch (Exception ex)
            {
                Debug.LogError($"Unable to process config {configPath}.  Loading defaults");
                Debug.LogError(ex);
                CreateDefaultConfig(configPath, settings);
            }
        }

        private static void CreateDefaultConfig(string configPath, JsonSerializerSettings settings)
        {
            Config = new ModConfig();
            File.WriteAllText(configPath, JsonConvert.SerializeObject(Config, settings));
        }
    }
}
