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
            statusType == CharacterAfflictions.STATUSTYPE.Weight ||
            !affliction.character.IsLocal ||
            (CharacterPatches.UpdateNormalStatuses.InUpdateNormalStatuses && statusType == CharacterAfflictions.STATUSTYPE.Cold)
        ) {
            return 0f; // add no multiplier to these ones
        }

        if (statusType == CharacterAfflictions.STATUSTYPE.Drowsy ||
            statusType == CharacterAfflictions.STATUSTYPE.Curse
        ) {
            return 1f; // only 2x it
        }

        return 2; // 3x the others
    }
}