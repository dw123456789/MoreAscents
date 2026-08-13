namespace MoreAscents;

public class FogGimmick : AscentGimmick {
    public override string GetDescription() {
        return "The fog starts earlier and moves faster.";
    }

    public override string GetTitle() {
        return "Chaos 2";
    }

    public override void OnFogInitNewSphere(OrbFogHandler fog) {
        fog.currentStartForward = 0;
        fog.currentStartHeight = 0;

        fog.speed *= 2;
        fog.maxWaitTime /= 10;
        Plugin.Logger.LogWarning("patched fog AGAIN");
    }
}