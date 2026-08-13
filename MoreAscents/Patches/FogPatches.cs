using HarmonyLib;

namespace MoreAscents.Patches;

public class FogPatches {
    [HarmonyPatch(typeof(OrbFogHandler), "InitNewSphere")]
    public static class Start {
        [HarmonyPostfix]
        public static void Postfix(OrbFogHandler __instance) {
            foreach (AscentGimmick gimmick in AscentGimmickHandler.gimmicks) {
                if (!gimmick.active)
                    continue;
                gimmick.OnFogInitNewSphere(__instance);
            }
        }
    }
}