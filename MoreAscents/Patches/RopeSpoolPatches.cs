using HarmonyLib;

namespace MoreAscents.Patches;

public class RopeSpoolPatches {
    [HarmonyPatch(typeof(RopeSpool), "OnInstanceDataSet")]
    public static class OnInstanceDataSet {
        [HarmonyPrefix]
        public static void Prefix(RopeSpool __instance) {

        }
    }
}