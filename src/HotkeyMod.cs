using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

                CommonButton[] commandButtons = contextMenu.commandButtons;
                //Hack to not use Harmony.  The menu buttons are recreated when the context menu is shown again.
                //Number the items.
                if (commandButtons.Length > 0 && !commandButtons[0].captionText.text.StartsWith($"{ModConfig.KeyStrings[0]}. "))
                {
                    for (int i = 0; i < commandButtons.Length; i++)
                    {
                        commandButtons[i].captionText.text =
                            $"{ModConfig.KeyStrings[i]}. {commandButtons[i].captionText.text}";
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

                if (buttonIndex != -1)
                {
                    if (buttonIndex > commandButtons.Length) return;

                    CommonButton commandButton = commandButtons[buttonIndex];

                    contextMenu.OnContextCommandClick(commandButton);
                }

            }

        }
    }
}
