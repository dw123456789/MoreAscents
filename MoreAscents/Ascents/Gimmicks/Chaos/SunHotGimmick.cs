using UnityEngine;
using Zorro.Core;

namespace MoreAscents;

public class SunHotGimmick : AscentGimmick {
    public override string GetDescription() {
        return "The sun is very hot, don't get a heatstroke.";
    }

    public override string GetTitle() {
        return "Chaos 3";
    }

    public override void OnUpdateNormalStatuses(Character character) {
        if (!character.IsLocal)
            return;
        if (DayNightManager.instance.isDay < 0.5f)
            return;
        var progressHandler = Singleton<MountainProgressHandler>.Instance;
        if (progressHandler == null || progressHandler.maxProgressPointReached >= 3)
            return;

        Vector3 sunDir = -RenderSettings.sun.transform.forward;

        RaycastHit hit = HelperFunctions.LineCheck(character.Center, character.Center + (sunDir * 1000), HelperFunctions.LayerType.AllPhysicalExceptCharacter);
        if (hit.transform && hit.transform.gameObject.name != "EdgeWall") {
            //Plugin.Logger.LogWarning(hit.transform.gameObject.name);
        } else {
            character.refs.afflictions.AddStatus(CharacterAfflictions.STATUSTYPE.Hot, 0.0025f * Time.deltaTime, false);
        }
    }
}