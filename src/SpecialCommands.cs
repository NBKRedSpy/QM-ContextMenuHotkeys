using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_ContextMenuHotkeys
{

    /// <summary>
    /// Context menu commands that are not put in the ContextMenuCommands enum.
    /// These are still used as Context Menu Commands.  I'm not sure why they are special.
    /// </summary>
    public enum SpecialCommands
    {
        RemoveAugmentation = 22,
        SplitStacks = 99_999,
        /// <summary>
        /// This doesn't currently work as the confirm is special.
        /// </summary>
        SplitStacksConfirm = 100_000,

        /// <summary>
        /// The Lock and Unlock toggle from the Filter pickup items mod
        /// https://steamcommunity.com/sharedfiles/filedetails/?id=3444150354
        /// </summary>
        LockItemsModToggle = 610_000
    }
}
