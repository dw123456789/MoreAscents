using MoreAscents.Patches;

namespace MoreAscents;

public class AfflictionGimmick : AscentGimmick {
    public override string GetTitle() {
        return "Ascent 10";
    }

    public override string GetDescription() {
        return "You're more vulnerable to everything but Injuries, Hunger and Weight.";
    }

    public override float AfflictionMultiplier(CharacterAfflictions affliction, CharacterAfflictions.STATUSTYPE statusType, float amount) {
        if (statusType == CharacterAfflictions.STATUSTYPE.Injury ||
            statusType == CharacterAfflictions.STATUSTYPE.Hunger ||
            statusType == CharacterAfflictions.STATUSTYPE.Weight || !affliction.character.IsLocal || (CharacterPatches.UpdateNormalStatuses.InUpdateNormalStatuses && statusType == CharacterAfflictions.STATUSTYPE.Cold))
            return 0f;
        return 2; //0.25f;
    }
}