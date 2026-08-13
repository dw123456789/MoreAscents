using System.Collections.Generic;
using UnityEngine;

namespace MoreAscents;

public class AscentGimmick {
    public virtual string GetTitle() {
        return "";
    }

    public virtual string GetDescription() {
        return "";
    }

    public virtual Color GetColor() {
        return Color.red;
    }

    public virtual string GetTitleReward() {
        return "";
    }

    public virtual void CharacterPassedOut(Character character) { }
    public virtual void OnLavaExisted(Lava lava) { }
    public virtual void OnFogInitNewSphere(OrbFogHandler __instance) { }
    public virtual void OnCharacterFall(Character character) { }
    public virtual void RespawnChestExisted(Spawner chest) { }
    public virtual void OnFinishCooking(ItemCooking itemCooking) { }
    public virtual void SpawnerSpawnItems(Spawner spawner, ref List<Transform> spawnSpots) { }
    public virtual void OnGrabbedCharacter(Character thisCharacter) { }
    public virtual void OnUpdateNormalStatuses(Character character) { }

    public virtual void Update() { }

    public virtual float AfflictionMultiplier(CharacterAfflictions afflictions, CharacterAfflictions.STATUSTYPE statusType, float amount) {
        return 0f;
    }

    public AscentStruct _ascentData;
    public bool active;
}