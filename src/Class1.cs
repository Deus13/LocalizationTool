using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;

namespace LocalizationTool.src
{
    public class Class1
    {
        [HarmonyPatch(typeof(LocalizedString))]
        [HarmonyPatch("Text")]

        internal class LocalizedString_Text
        {

            private static void Postfix(LocalizedString __instance, ref string __result)
            {
                if (Utils.keys.Count == 0) Utils.OnLoad();

                Utils.Log(__instance.m_LocalizationID);
                string tmp;
                Utils.Log(__result+"  "+ Utils.keys.Count.ToString()+"  "+ Utils.objList.Count.ToString());
                if (Utils.keys.TryGetValue(__instance.m_LocalizationID,out tmp)) __result = tmp;
                Utils.Log(__result);
            }
        }
    }
}
