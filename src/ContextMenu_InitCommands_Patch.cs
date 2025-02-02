using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MGSC;

namespace QM_ContextMenuHotkeys
{
    public static class ContextMenu_InitCommands_Patch
    {
        /// <summary>
        /// Adds any hotkeys that are defined to the text of the Context Menu options.
        /// Ex: E for Eat
        /// </summary>
        /// <param name="__instance"></param>
        public static void AddHotkeyHighlightToButtons(BaseContextMenu __instance)
        {

            HashSet<ContextMenuCommand> modifierCommands = Plugin.Config.ModifierCommands;

            int menuIndex = 0;

            foreach (var commandBind in __instance._commandBinds)
            {
                CommonButton commandButton = commandBind.Key;
                ContextMenuCommand command = (ContextMenuCommand)commandBind.Value;

                string hotkeyText = "";
                if(Plugin.Config.EnableNumberedMode)
                {
                    hotkeyText = ModConfig.KeyStrings[menuIndex];

                    if (hotkeyText != "")
                    {
                        hotkeyText += modifierCommands.Contains(command) ?
                            "+ " : " ";
                    }
                }

                if (Plugin.Config.EnableCommandMode)
                {
                    string keyString;

                    //Key bindings if available.
                    if (ModConfig.KeyBindStrings.TryGetValue(command, out keyString))
                    {
                        hotkeyText = keyString + " " + hotkeyText;
                    }
                }

                commandButton.captionText.text = $"<color=\"yellow\">{hotkeyText}</color>{commandButton.captionText.text}";
            }
        }
    }
}
