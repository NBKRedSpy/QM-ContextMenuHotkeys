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

            harmony.Patch(AccessTools.Method(typeof(InputHelper), nameof(InputHelper.GetKeyDown), null),
                null, new HarmonyMethod(typeof(InputHelperPatches), nameof(GetKeyNoCodePostfix))
                );

        }


        public static void GetKeyNoCodePostfix(ref KeyCode __result)
        {

            //Debug.Log($"GetKeyNoCodePostfix {__result}");

            switch (__result)
            {
                case KeyCode.None:
                case KeyCode.Menu:
                case KeyCode.Escape:
                    return;
            }

            if(IsContextWindowOpen())
            {
                __result = KeyCode.None;
            }

        }
        //public static bool GetKeyNoCode()
        //{

        //    if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.Menu)) return true;

        //    bool contextIsOpen = IsContextWindowOpen();

        //    if (IsContextWindowOpen())
        //    {
        //        Debug.Log($"{DateTime.Now.Ticks} {InputHelper.GetKey()}")
        //    }


        //    return !contextIsOpen;
        //}




        public static bool GetKeyPrefix(KeyCode keyCode)
        {
            if(keyCode == KeyCode.Menu || keyCode == KeyCode.Escape) return true;

            //Debug.Log($"GetKeyPrefix {keyCode}");

            return !IsContextWindowOpen();
        }

        public static bool IsContextWindowOpen()
        {

            return SingletonMonoBehaviour<SpaceUI>.Instance?.NoPlayerContextMenu.IsActiveView == true
                || DungeonUI.Instance?.ContextMenu.IsActiveView == true;
        }
    }
}
