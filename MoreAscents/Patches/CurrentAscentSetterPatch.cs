using HarmonyLib;

namespace MoreAscents.Patches;

[HarmonyPatch(typeof(Ascents), nameof(Ascents.currentAscent), MethodType.Setter)]
public class CurrentAscentSetterPatch {
    [HarmonyPrefix]
    public static void Prefix(int value) {
        AscentGimmickHandler.DisableGimmicks();
        AscentGimmickHandler.MarkGimmicksAsActive(value + 2);
    }
}