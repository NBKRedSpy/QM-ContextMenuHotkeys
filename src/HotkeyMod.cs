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
            ActiveCommandBinds = config.CommandBinds.Where(x => x.Key != KeyCode.None).ToList();
        }

        /// <summary>
        /// Handle in combat screens.
        /// </summary>
        /// <param name="context"></param>
        [Hook(ModHookType.DungeonUpdateAfterGameLoop)]
        public static void DungeonUpdateAfterGameLoop(IModContext context)
        {
            CommonContextMenu contextMenu = (CommonContextMenu) UI.GetActiveViews().FirstOrDefault(x => x is CommonContextMenu);

            HandleMenuContext(contextMenu);
        }

        /// <summary>
        /// Handles all cargo type screens.
        /// </summary>
        /// <param name="context"></param>
        [Hook(ModHookType.SpaceUpdateBeforeGameLoop)]
        public static void SpaceUpdateBeforeGameLoop(IModContext context)
        {
            CommonContextMenu contextMenu = (CommonContextMenu)UI.GetActiveViews().FirstOrDefault(x => x is CommonContextMenu);

            HandleMenuContext(contextMenu);
        }

        public static void HandleMenuContext(CommonContextMenu contextMenu)
        {
            if (contextMenu is null || !contextMenu.isActiveAndEnabled) return;

            List<ContextMenuCommand> menuCommandBinds = contextMenu._commandBinds.Values
                .Cast<ContextMenuCommand>().ToList();

            List<CommonButton> menuCommandButtons = new List<CommonButton>(contextMenu._commandBinds.Keys);

            CommonButton targetButton = null;

            //----- Command binds check
            targetButton = GetCommandKeyBindTarget(menuCommandButtons, menuCommandBinds);

            if (targetButton == null) return;

            contextMenu.OnContextCommandClick(targetButton,1);

        }

        /// <summary>
        /// Checks if the Input.GetKeyUp matches one of the hotkey binds.
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
