using System.Reflection;

namespace MoreAscents;

public class FallDamageGimmick : AscentGimmick
{
    public override string GetTitle() {
        return "Ascent 9";
    }
    
    public override string GetDescription()
    {
        return "Butter Fingers.";
    }

    public override void OnCharacterFall(Character character) {
        if (!character.IsLocal)
            return;
        // using character here seems to not affect the local character and i have zero clue why
        
        MethodInfo info = Character.localCharacter.refs.items.GetType().GetMethod("DropAllItems", BindingFlags.Instance | BindingFlags.NonPublic);
        info.Invoke(Character.localCharacter.refs.items, [true]);
    }
}