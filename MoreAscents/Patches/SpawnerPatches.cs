using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace MoreAscents.Patches;

public class SpawnerPatches {
    [HarmonyPatch(typeof(Spawner), "SpawnItems")]
    public static class GetSpawnSpots {
        [HarmonyPrefix]
        public static void Prefix(Spawner __instance, ref List<Transform> spawnSpots) {
            foreach (AscentGimmick gimmick in AscentGimmickHandler.gimmicks) {
                if (!gimmick.active)
                    continue;
                gimmick.SpawnerSpawnItems(__instance, ref spawnSpots);
            }
        }
    }

    [HarmonyPatch(typeof(Luggage), "Awake")]
    public static class Awake {
        [HarmonyPostfix]
        public static void Postfix(Luggage __instance) {
            if (__instance.GetType() != typeof(RespawnChest))
                return;

            foreach (AscentGimmick gimmick in AscentGimmickHandler.gimmicks) {
                if (!gimmick.active)
                    continue;
                gimmick.RespawnChestExisted(__instance);
            }
        }
    }
}