using HarmonyLib;

namespace MoreAscents.Patches;

public class GUIManagerPatches {
    [HarmonyPatch(typeof(GUIManager), "Grasp")]
    public static class Grasp {
        // bandaid asf but whatever man
        public static float SinceLastGrab = 0;

        [HarmonyPrefix]
        public static void Prefix() {
            if (!CharacterPatches.Reach.character.IsLocal) {
                Plugin.Logger.LogWarning("not local, not firing OnGrabbedCharacter to gimmicks.");
                return;
            }

            if (SinceLastGrab <= 1f) {
                return;
            }

            SinceLastGrab = 0f;

            Plugin.Logger.LogInfo("success");

            foreach (AscentGimmick gimmick in AscentGimmickHandler.gimmicks) {
                if (!gimmick.active)
                    continue;
                gimmick.OnGrabbedCharacter(Character.localCharacter);
            }
        }
    }
}