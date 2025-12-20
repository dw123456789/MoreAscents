using HarmonyLib;

namespace MoreAscents.Patches;

internal class BingBongPatches
{

    [HarmonyPatch((typeof(BingBongMouth)),"Update")]
    
    private static class Update {
        
        internal static void Prefix(BingBongMouth __instance) {
            Item item = __instance.GetComponent<Item>();
            if (item.itemState == ItemState.Held) {
                BingBongMechanics.BingBongUnheldFor = 0;
            }
        }
    }
}
}
