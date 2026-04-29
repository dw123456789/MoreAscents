using HarmonyLib;
using System;

namespace MoreAscents.Patches;

internal static class AchievementManagerPatches
{
    [HarmonyPatch(typeof(AchievementManager), nameof(AchievementManager.GetMaxAscent))]
    internal static class GetMaxAscent
    {
        [HarmonyPostfix]
        internal static void Postfix(ref int __result) {
            __result = Plugin.ascentsUnlocked.Value;
            if (__result > AscentData.Instance.ascents.Count-1) {
                __result = AscentData.Instance.ascents.Count-1;
            }
        }
    }
    
    [HarmonyPatch(typeof(AchievementManager),"Start")]
    internal static class Start {
        [HarmonyPrefix]
        internal static void Prefix() {
            Plugin.Init();
        }
    }
    
    [HarmonyPatch(typeof(AchievementManager), nameof(AchievementManager.SetSteamStat), new Type[] { typeof(STEAMSTATTYPE), typeof(int) })]
    internal static class SetSteamStat {
        [HarmonyPrefix]
        internal static void Prefix(STEAMSTATTYPE steamStatType, ref int value) {
            if (steamStatType != STEAMSTATTYPE.MaxAscent)
                return;
            
            Plugin.ascentsUnlocked.Value = value;
            if (value > AscentGimmickHandler.BaseAscents-1) {
                value = AscentGimmickHandler.BaseAscents-1;
            }
        }
    }
}
