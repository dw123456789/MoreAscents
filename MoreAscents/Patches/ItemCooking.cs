using HarmonyLib;

namespace MoreAscents.Patches;

public class ItemCookingPatches {
    [HarmonyPatch(typeof(ItemCooking), "FinishCookingImpl")]
    public static class FinishCookingRPC {
        [HarmonyPrefix]
        public static void Prefix(ItemCooking __instance) {
            foreach (AscentGimmick gimmick in AscentGimmickHandler.gimmicks) {
                if (!gimmick.active)
                    continue;
                gimmick.OnFinishCooking(__instance);
            }
        }
    }
}