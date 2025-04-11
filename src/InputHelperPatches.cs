using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_ContextMenuHotkeys
{
    public static class InputHelperPatches
    {

        //public static MGSC.ContextMenu contextMenu = SingletonMonoBehaviour<SpaceUI>.Instance.NoPlayerContextMenu;

        public static void Patch(Harmony harmony)
        {
            harmony.Patch(AccessTools.Method(typeof(InputHelper), nameof(InputHelper.GetKey)),
                new HarmonyMethod(typeof(InputHelperPatches), nameof(GetKeyPrefix))
                );

            harmony.Patch(AccessTools.Method(typeof(InputHelper), nameof(InputHelper.GetKeyDown), new Type[] {typeof(KeyCode)}),
                new HarmonyMethod(typeof(InputHelperPatches), nameof(GetKeyPrefix)) 
                );

        }

        public static bool GetKeyPrefix(KeyCode keyCode)
        {
            if(keyCode == KeyCode.Menu || keyCode == KeyCode.Escape) return true;

            return !IsContextWindowOpen();
        }

        public static bool IsContextWindowOpen()
        {
            return UI.GetActiveViews().Any(x => x is CommonContextMenu);
        }
    }
}
