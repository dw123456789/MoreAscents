using HarmonyLib;

namespace MoreAscents.Patches;

internal static class CharacterCustomizationPatches {
    [HarmonyPatch(typeof(CharacterCustomization), nameof(CharacterCustomization.SetCharacterSash))]
    internal static class SetCharacterSash {
        [HarmonyPrefix]
        internal static void Prefix(ref int index) {
            if (index > AscentGimmickHandler.BaseAscents - 2) {
                index = AscentGimmickHandler.BaseAscents - 2;
            }
        }
    }
}