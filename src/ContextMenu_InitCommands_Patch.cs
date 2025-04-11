using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MGSC;
using UnityEngine.Experimental.Rendering;

namespace QM_ContextMenuHotkeys
{


    [HarmonyPatch(typeof(CommonContextMenu), nameof(CommonContextMenu.SetupCommand))]
    public static class ContextMenu_InitCommands_Patch
    {
        /// <summary>
        /// Adds any hotkeys that are defined to the text of the Context Menu options.
        /// Ex: E for Eat
        /// </summary>
        /// <param name="__instance"></param>
        public static void Postfix(CommonContextMenu __instance, int bindedVal, bool interactable)
        {

            if (!interactable) return;

            //The SetupCommand loops through all of the buttons each time it is adding a new command (via SetupCommand).
            //I think it is trying to find a context menu CommonButton that hasn't been used yet.
            //I'm guessing that there will never be an existing context menu button that isn't available.
            //Probably pre-allocated.

            //The added button will the last item in the _commandBinds list, even if it is not enabled (not interactable).

            CommonButton commandButton = __instance._activeButtonsList.Last();

            string keyString;

            //Note that currently the game doesn't always use the ContextMenuCommand.  As of v0.8.6,
            //Split is 999999, and SplitStacks is 100000.  Don't know why this was moved out
            //of the ContextMenuCommand enum.

            if (!ModConfig.KeyBindStrings.TryGetValue((ContextMenuCommand)bindedVal, out keyString)) return;

            string hotkeyText = keyString + " ";

            commandButton.captionText.text = $"<color=\"yellow\">{hotkeyText}</color>{commandButton.captionText.text}";
        }
    }
}
