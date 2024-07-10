using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
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

        public void InitKeyStrings()
        {

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
                default:
                    return code.ToString();
            }
        }
    }
}