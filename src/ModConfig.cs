using MGSC;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_ContextMenuHotkeys
{

    public class ModConfig
    {

        public const string LatestConfigVersion = "2.0";

        /// <summary>
        /// The string version of the hotkeys.
        /// Necessary since 0-9 are prefixed with Alpha
        /// </summary>
        public static List<string> KeyStrings { get; private set; }

        /// <summary>
        /// The shortcut keys by context menu command.
        /// </summary>
        public static Dictionary<ContextMenuCommand, string> KeyBindStrings { get; private set; }


        /// <summary>
        /// The configuration version.  Used to assist with conversion
        /// </summary>
        public string ConfigVersion;
        public List<CommandBindKey> CommandBinds { get; set; }

        public ModConfig()
        {
            //Defaults
            CommandBinds = new List<CommandBindKey>()
            {
                new CommandBindKey(KeyCode.X, ContextMenuCommand.Disassemble),
                new CommandBindKey(KeyCode.X, ContextMenuCommand.DisassembleAll),
                new CommandBindKey(KeyCode.O, ContextMenuCommand.DisassembleX1),
                new CommandBindKey(KeyCode.D, ContextMenuCommand.Drop),
                new CommandBindKey(KeyCode.E, ContextMenuCommand.Equip),
                new CommandBindKey(KeyCode.E, ContextMenuCommand.Eat),
                new CommandBindKey(KeyCode.R, ContextMenuCommand.Reload),
                new CommandBindKey(KeyCode.T, ContextMenuCommand.Take),
                new CommandBindKey(KeyCode.Q, ContextMenuCommand.Unequip),
                new CommandBindKey(KeyCode.W, ContextMenuCommand.UnloadAmmo),
                //Split Stacks is a hardcoded value now and no longer in the enum.  
                //I don't know why it was moved out.
                new CommandBindKey(KeyCode.V, (ContextMenuCommand)SpecialCommands.SplitStacks),
                new CommandBindKey(KeyCode.Alpha3, (ContextMenuCommand)SpecialCommands.LockItemsModToggle)
            };

            //Add any binds that are not set.  This is to assist users setting up the commands without having to 
            //  search through them.
            AddMissingCommands();
        }

        /// <summary>
        /// Adds any binding commands that do not already have an entry in the binding list.
        /// </summary>
        /// <param name="existing"></param>
        /// <returns></returns>
        public void AddMissingCommands()
        {

            HashSet<ContextMenuCommand> existingCommands = new HashSet<ContextMenuCommand>(CommandBinds.Select(x => x.Command));

            List<CommandBindKey> newCommands =
                Enum.GetValues(typeof(ContextMenuCommand)).Cast<ContextMenuCommand>()
                .Where(x => !existingCommands.Contains(x))
                .Select(x => new CommandBindKey(KeyCode.None, x))
                .ToList();

            CommandBinds.AddRange(newCommands);
        }

        public void InitKeyStrings()
        {
            KeyBindStrings = CommandBinds.ToDictionary(x => x.Command, x => FormatKeyCode(x.Key));
        }

        public string FormatKeyCode(KeyCode code)
        {
            switch (code)
            {
                case KeyCode.Alpha0:
                    return "0";
                case KeyCode.Alpha1:
                    return "1";
                case KeyCode.Alpha2:
                    return "2";
                case KeyCode.Alpha3:
                    return "3";
                case KeyCode.Alpha4:
                    return "4";
                case KeyCode.Alpha5:
                    return "5";
                case KeyCode.Alpha6:
                    return "6";
                case KeyCode.Alpha7:
                    return "7";
                case KeyCode.Alpha8:
                    return "8";
                case KeyCode.Alpha9:
                    return "9";
                case KeyCode.Keypad0:
                    return "Num0";

                case KeyCode.Keypad1:
                    return "Num1";
                case KeyCode.Keypad2:
                    return "Num2";
                case KeyCode.Keypad3:
                    return "Num3";
                case KeyCode.Keypad4:
                    return "Num4";
                case KeyCode.Keypad5:
                    return "Num5";
                case KeyCode.Keypad6:
                    return "Num6";
                case KeyCode.Keypad7:
                    return "Num7";
                case KeyCode.Keypad8:
                    return "Num8";
                case KeyCode.Keypad9:
                    return "Num9";
                case KeyCode.None:
                    return "";
                default:
                    return code.ToString();
            }
        }

        /// <summary>
        /// Used to re-write the config file to remove any settings that are no longer used
        /// and new defaults.
        /// This is a bit of a heavy handed hack to get the config file to be up to date.
        /// </summary>
        /// <param name="configPath">The path to the config file</param>
        /// <param name="existingText">The text that was loaded from the config file on disk.</param>
        public void RewriteConfigFileIfDifferent(string configPath, string existingJson)
        {
            string newJson = JsonConvert.SerializeObject(this, Plugin.JsonSettings);

            if (existingJson == newJson) return;

            File.WriteAllText(configPath, newJson);
        }

    }
}