using MGSC;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_ContextMenuHotkeys
{

    /// <summary>
    /// A key mapping to a menu command.
    /// Ex:  D to the Drop
    /// </summary>
    public class CommandBindKey
    {

        [JsonConverter(typeof(StringEnumConverter))]
        public KeyCode  Key { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ContextMenuCommand Command  { get; set; }

        public CommandBindKey()
        {
            
        }

        public CommandBindKey(KeyCode key, ContextMenuCommand command)
        {
            Key = key;
            Command = command;
        }
    }
}
