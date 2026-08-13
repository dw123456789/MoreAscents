using System.Collections.Generic;
using MoreAscents.Patches;
using UnityEngine;

namespace MoreAscents;

public class HelpingIsBadGimmick : AscentGimmick
{
    public override string GetTitle() {
        return "Ascent 12";
    }
    
    public override string GetDescription() {
        return "Helping makes you drowsy.";
    }

    public override void OnGrabbedCharacter(Character character) {
        character.refs.afflictions.AddStatus(CharacterAfflictions.STATUSTYPE.Drowsy, 0.05f);
    }
}