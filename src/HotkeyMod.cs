using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace QM_ContextMenuHotkeys
{
    public class HotkeyMod
    {
        /// <summary>
        /// Handle in combat screens.
        /// </summary>
        /// <param name="context"></param>
        [Hook(ModHookType.DungeonUpdateAfterGameLoop)]
        public static void DungeonUpdateAfterGameLoop(IModContext context)
        {
            MGSC.ContextMenu contextMenu = DungeonGameMode.Instance._dungeonUI.ContextMenu;

            HandleMenuContext(contextMenu);
        }

        /// <summary>
        /// Handles all cargo type screens.
        /// </summary>
        /// <param name="context"></param>
        [Hook(ModHookType.SpaceUpdateBeforeGameLoop)]
        public static void SpaceUpdateBeforeGameLoop(IModContext context)
        {
            MGSC.ContextMenu contextMenu = SingletonMonoBehaviour<SpaceUI>.Instance.NoPlayerContextMenu;

            HandleMenuContext(contextMenu);
        }

        public static void HandleMenuContext(MGSC.ContextMenu contextMenu)
        {
            if (contextMenu.IsActiveView)
            {

                HashSet<ContextMenuCommand> modifierCommands = Plugin.Config.ModifierCommands;

                CommonButton[] commandButtons = contextMenu.commandButtons;
                //Hack to not use Harmony.  The menu buttons are recreated when the context menu is shown again.
                //Prefix the command text.

                //The command binds array will match the buttons array
                List<ContextMenuCommand> commandBinds = contextMenu._commandBinds.Values.ToList();
                if (commandBinds.Count > 0 && !commandButtons[0].captionText.text.StartsWith($"{ModConfig.KeyStrings[0]}"))
                {
                    for (int i = 0; i < commandBinds.Count && commandBinds.Count < 10; i++)
                    {
                        //Shift only command
                        string shiftText = modifierCommands.Contains(commandBinds[i]) ?
                            "+" : "";

                        commandButtons[i].captionText.text =
                            $"{ModConfig.KeyStrings[i]}{shiftText}. {commandButtons[i].captionText.text}";
                    }
                }

                int buttonIndex =
                    Input.GetKeyUp(Plugin.Config.Command1) ? 0 :
                    Input.GetKeyUp(Plugin.Config.Command2) ? 1 :
                    Input.GetKeyUp(Plugin.Config.Command3) ? 2 :
                    Input.GetKeyUp(Plugin.Config.Command4) ? 3 :
                    Input.GetKeyUp(Plugin.Config.Command5) ? 4 :
                    Input.GetKeyUp(Plugin.Config.Command6) ? 5 :
                    Input.GetKeyUp(Plugin.Config.Command7) ? 6 :
                    Input.GetKeyUp(Plugin.Config.Command8) ? 7 :
                    Input.GetKeyUp(Plugin.Config.Command9) ? 8 :
                    Input.GetKeyUp(Plugin.Config.Command10) ? 9 : -1
                    ;


                //Broken out to help with key debugging.
                if (buttonIndex == -1) { return; }

                //Shift commands
                if (buttonIndex >= commandBinds.Count) return;

                if (modifierCommands.Contains(commandBinds[buttonIndex]))
                {
                    //This command requires a shift.
                    if(!Plugin.Config.ModifierKeys.Any(x=> Input.GetKey(x)))
                    {
                        return;
                    }
                }

                CommonButton commandButton = commandButtons[buttonIndex];

                contextMenu.OnContextCommandClick(commandButton);

            }
        }
    }
}
