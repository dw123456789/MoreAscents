using HarmonyLib;

namespace MoreAscents.Patches;

public class LavaRiverPatches {
    [HarmonyPatch(typeof(Lava), "Start")]
    public static class Start {
        [HarmonyPrefix]
        public static void Prefix(Lava __instance) {
            foreach (AscentGimmick gimmick in AscentGimmickHandler.gimmicks) {
                if (!gimmick.active)
                    continue;
                gimmick.OnLavaExisted(__instance);
            }
        }
    }
}