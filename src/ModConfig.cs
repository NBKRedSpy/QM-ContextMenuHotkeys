using MGSC;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_ContextMenuHotkeys
{
    public class ModConfig
    {
        /// <summary>
        /// The string version of the hotkeys.
        /// Necessary since 0-9 are prefixed with Alpha
        /// </summary>
        public static List<string> KeyStrings { get; private set; }

        /// <summary>
        /// The shortcut keys by context menu command.
        /// </summary>
        public static Dictionary<ContextMenuCommand, string> KeyBindStrings { get; private set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public KeyCode Command1 { get; set; } = KeyCode.Alpha1;
        [JsonConverter(typeof(StringEnumConverter))]
        public KeyCode Command2 { get; set; } = KeyCode.Alpha2;
        [JsonConverter(typeof(StringEnumConverter))]
        public KeyCode Command3 { get; set; } = KeyCode.Alpha3;
        [JsonConverter(typeof(StringEnumConverter))]
        public KeyCode Command4 { get; set; } = KeyCode.Alpha4;
        [JsonConverter(typeof(StringEnumConverter))]
        public KeyCode Command5 { get; set; } = KeyCode.Alpha5;
        [JsonConverter(typeof(StringEnumConverter))]
        public KeyCode Command6 { get; set; } = KeyCode.Alpha6;
        [JsonConverter(typeof(StringEnumConverter))]
        public KeyCode Command7 { get; set; } = KeyCode.Alpha7;
        [JsonConverter(typeof(StringEnumConverter))]
        public KeyCode Command8 { get; set; } = KeyCode.Alpha8;
        [JsonConverter(typeof(StringEnumConverter))]
        public KeyCode Command9 { get; set; } = KeyCode.Alpha9;
        [JsonConverter(typeof(StringEnumConverter))]
        public KeyCode Command10 { get; set; } = KeyCode.Alpha0;


        public List<CommandBindKey> CommandBinds { get; set; }

        /// <summary>
        /// If true, will disable the patch that blocks the game's input handler.
        /// </summary>
        public bool DisableKeyLock { get; set; } = false;

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
            };
        }

        [JsonConverter(typeof(JsonArrayEnumConverter<HashSet<ContextMenuCommand>, ContextMenuCommand>))]
        public HashSet<ContextMenuCommand> ModifierCommands { get; set; } = new HashSet<ContextMenuCommand>()
        {
            ContextMenuCommand.Disassemble,
            ContextMenuCommand.DisassembleX1,
            ContextMenuCommand.DisassembleAll,
            ContextMenuCommand.UnlockDatadisk,
        };

        /// <summary>
        /// The modifier keys required for ModiferCommands
        /// </summary>
        [JsonConverter(typeof(JsonArrayEnumConverter<List<KeyCode>, KeyCode>))]
        public List<KeyCode> ModifierKeys { get; set; } = new List<KeyCode>()
        {
            KeyCode.LeftShift,
            KeyCode.RightShift,
            KeyCode.LeftAlt,
            KeyCode.RightAlt
        };

        public void InitKeyStrings()
        {
            KeyBindStrings = CommandBinds.ToDictionary(x => x.Command, x => FormatKeyCode(x.Key));

            KeyStrings = new List<string>()
            {
                FormatKeyCode(Command1),
                FormatKeyCode(Command2),
                FormatKeyCode(Command3),
                FormatKeyCode(Command4),
                FormatKeyCode(Command5),
                FormatKeyCode(Command6),
                FormatKeyCode(Command7),
                FormatKeyCode(Command8),
                FormatKeyCode(Command9),
                FormatKeyCode(Command10),
            };
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
    }
}