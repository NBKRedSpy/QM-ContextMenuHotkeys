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
        public static void Postfix(ContextMenu __instance)
        {

            HashSet<ContextMenuCommand> modifierCommands = Plugin.Config.ModifierCommands;

            //The command binds array will match the buttons array
            List<ContextMenuCommand> commandBinds = __instance._commandBinds.Values.ToList();

            for (int i = 0; i < __instance._activeButtonsList.Count; i++)
            {
                CommonButton command = __instance._activeButtonsList[i];

                string hotkeyText = "";
                if(Plugin.Config.EnableNumberedMode)
                {
                    hotkeyText = ModConfig.KeyStrings[i];

                    if (hotkeyText != "")
                    {
                        hotkeyText += modifierCommands.Contains(commandBinds[i]) ?
                            "+ " : " ";
                    }
                }


                if (Plugin.Config.EnableCommandMode)
                {
                    string keyString;

                    //Key bindings if available.
                    if (ModConfig.KeyBindStrings.TryGetValue(commandBinds[i], out keyString))
                    {
                        hotkeyText = keyString + " " + hotkeyText;
                    }
                }

                command.captionText.text = $"<color=\"yellow\">{hotkeyText}</color>{command.captionText.text}";
            }
        }
    }
}
