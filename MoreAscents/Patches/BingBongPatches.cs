using HarmonyLib;

namespace MoreAscents.Patches;

internal class BingBongPatches
{
    internal static Item heldBingBongItem = null;
    [HarmonyPatch(typeof(CharacterData), nameof(CharacterData.currentItem), MethodType.Setter)]
    internal static class Patch {
        [HarmonyPrefix]
        internal static void Prefix(Item value) {
            //if (value == null) {
            //    Debug.LogWarning("MoreAscents::UnequippedItem");
            //}
            //Debug.LogWarning($"MoreAscents_test::Equipped: "+value?.UIData?.itemName ?? "null");
            if (value != null && (value?.UIData?.itemName?.ToLowerInvariant()?.Equals("bing bong") == true)) {
                //Debug.LogWarning($"MoreAscents::Equipped");
                heldBingBongItem = value;
            } else {
                heldBingBongItem = null; 
            }
        }
    }
}