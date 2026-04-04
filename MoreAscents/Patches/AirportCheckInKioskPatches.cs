using HarmonyLib;

namespace MoreAscents.Patches;

public class AirportCheckInKioskPatches
{
    [HarmonyPatch(typeof(AirportCheckInKiosk), nameof(AirportCheckInKiosk.BeginIslandLoadRPC))]
    public static class LoadIslandMaster
    {
        [HarmonyPrefix]
        public static void Prefix(int ascent) {
            AscentGimmickHandler.DisableGimmicks();
            AscentGimmickHandler.MarkGimmicksAsActive(ascent+2);
        }
    }
}