using MoreAscents.API;
using MoreAscents.Patches;
using UnityEngine;

namespace MoreAscents;

public class BingBongGimmick : AscentGimmick {
    public override string GetDescription() {
        return "Bing Bong calls upon an evil force if not given attention.";
    }

    public override string GetTitle() {
        return "Chaos 4";
    }

    private static float SinceLastShake = 0;

    public override void Update() {
        var dt = Time.deltaTime;
        SinceLastShake += dt;

        var bingBong = BingBongPatches.heldBingBongItem;
        bool bingBongIsHeld = bingBong != null && bingBong.itemState == ItemState.Held;
        if (bingBongIsHeld) {
            BingBongMechanics.BingBongUnheldFor = 0;
        } else {
            BingBongMechanics.BingBongUnheldFor += dt;
        }

        foreach (Character character in Character.AllCharacters) {
            if (!character.IsLocal) {
                continue;
            }
            if (character.data.passedOutOnTheBeach > 0) {
                BingBongMechanics.BingBongUnheldFor = 0;
                break;
            }

            // shake the screen if bingbong is about to be angry
            if (BingBongMechanics.BingBongUnheldFor > 10 && BingBongMechanics.BingBongUnheldFor <= 15) {
                if (SinceLastShake > 1 / 60) {
                    SinceLastShake = 0;
                    GamefeelHandler.instance.AddPerlinShake(0.7f, 0.15f, 15f);
                }
            }

            // if unheld for too long, apply status effect
            if (BingBongMechanics.BingBongUnheldFor >= 15) {
                character.refs.afflictions.AddStatus(CharacterAfflictions.STATUSTYPE.Crab, 0.05f * Time.deltaTime, false);
                break;
            }
        }
    }
}