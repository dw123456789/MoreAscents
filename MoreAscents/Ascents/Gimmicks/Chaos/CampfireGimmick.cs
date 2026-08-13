namespace MoreAscents;

public class CampfireGimmick : AscentGimmick {
    public override string GetDescription() {
        return "Campfires are extremely hot.";
    }

    public override string GetTitle() {
        return "Chaos 2";
    }

    public override void OnFinishCooking(ItemCooking itemCooking) {
        itemCooking.wreckWhenCooked = true;
    }
}