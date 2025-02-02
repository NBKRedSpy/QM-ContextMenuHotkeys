using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace QM_ContextMenuHotkeys
{
    public static class HotkeyMod
    {

        /// <summary>
        /// Only the command binds that have a key set (not Key.None)
        /// Empty if the mode is disabled.
        /// </summary>
        private static List<CommandBindKey> ActiveCommandBinds { get; set; }

        public static void Init(ModConfig config)
        {
            if(config.EnableCommandMode)
            {
                ActiveCommandBinds = config.CommandBinds.Where(x => x.Key != KeyCode.None).ToList();
            }
        }

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


        private static CommonButton PreviousFirstButton = null;

        public static void HandleMenuContext(MGSC.ContextMenu contextMenu)
        {
            if (!contextMenu.IsActiveView) return;

            List<ContextMenuCommand> menuCommandBinds = contextMenu._commandBinds.Values
                .Cast<ContextMenuCommand>().ToList();

            List<CommonButton> menuCommandButtons = new List<CommonButton>(contextMenu._commandBinds.Keys);

            CommonButton targetButton = null;

            if(Plugin.Config.EnableCommandMode)
            {
                //----- Command binds check
                targetButton = GetCommandKeyBindTarget(menuCommandButtons, menuCommandBinds);
            }

            if (Plugin.Config.EnableNumberedMode && targetButton == null)
            {
                HashSet<ContextMenuCommand> modifierCommands = Plugin.Config.ModifierCommands;
                targetButton = GetPositionalBind(modifierCommands, menuCommandButtons, menuCommandBinds);
            }

            if (targetButton == null) return;

            contextMenu.OnContextCommandClick(targetButton,1);

        }

        /// <summary>
        /// Checks for a positional keybind.
        /// Returns the button if there is a match.
        /// </summary>
        /// <param name="modifierCommands"></param>
        /// <param name="menuCommandButtons"></param>
        /// <param name="menuCommandBinds"></param>
        /// <param name=""></param>
        /// <returns></returns>
        private static CommonButton GetPositionalBind(HashSet<ContextMenuCommand> modifierCommands, List<CommonButton> menuCommandButtons, 
            List<ContextMenuCommand> menuCommandBinds)
        {
            //The command binds array will match the buttons array
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

            if (buttonIndex == -1 || buttonIndex >= menuCommandBinds.Count) return null;

            //Check if the command requires a modifier.  Generally Shift or Alt + <number>
            if (modifierCommands.Contains(menuCommandBinds[buttonIndex]))
            {
                //This command requires a shift.
                if (!Plugin.Config.ModifierKeys.Any(x => Input.GetKey(x)))
                {
                    return null;
                }
            }

            return menuCommandButtons[buttonIndex];
        }

        /// <summary>
        /// Searches the list of command based bindings for a match.
        /// If found, returns the matching button.  Else null.
        /// </summary>
        /// <param name="menuCommandButtons"></param>
        /// <param name="menuCommandBinds"></param>
        /// <param name=""></param>
        /// <returns></returns>
        private static CommonButton GetCommandKeyBindTarget(List<CommonButton> menuCommandButtons, 
            List<ContextMenuCommand> menuCommandBinds)
        {
            //there may be more than one command with the same hotkey.  For example, the three disassembles.
            List<CommandBindKey> matchingBinds = ActiveCommandBinds.Where(x => Input.GetKeyUp(x.Key)).ToList();

            if(matchingBinds.Count == 0) return null;

            int commandIndex = menuCommandBinds.FindIndex(x => matchingBinds.Any(y => y.Command == x));

            return (commandIndex == -1) ? null : menuCommandButtons[commandIndex]; 
        }
    }
}
