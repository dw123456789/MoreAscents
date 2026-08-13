using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreAscents.Patches;
internal class DisableAscent7
{
    [HarmonyPatch(typeof(Ascents), "canReviveDead", MethodType.Getter)]
    public static class Patch
    {
        [HarmonyPostfix]
        public static void Postfix(ref bool __result)
        {
            if (!MoreAscents.Plugin.ascent13Disabler.Value)
                return;
            __result = true;
        }
    }
}