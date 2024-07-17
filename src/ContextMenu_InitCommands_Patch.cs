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
    [HarmonyPatch(typeof(ContextMenu), nameof(ContextMenu.InitCommands))]
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

                string positionalString = "";
                positionalString = ModConfig.KeyStrings[i];

                if(positionalString != "")
                {
                    positionalString += modifierCommands.Contains(commandBinds[i]) ?
                        "+ " : " ";
                }

                string keyString;
                
                if(ModConfig.KeyBindStrings.TryGetValue(commandBinds[i], out keyString))
                {
                    positionalString = keyString + " " + positionalString;
                }

                command.captionText.text = $"{positionalString}{command.captionText.text}";
            }
        }
    }
}
