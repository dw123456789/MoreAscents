using System.Collections.Generic;
using MoreAscents.API;
using MoreAscents.Patches;
using UnityEngine;
using Zorro.Core;

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
        SinceLastShake += Time.deltaTime;


        foreach (Character character in Character.AllCharacters) {
            if (!character.IsLocal) {
                continue;
            }
            if (character.data.passedOutOnTheBeach > 0) {
                BingBongMechanics.BingBongUnheldFor = 0;
                break;
            }
            
            if (BingBongMechanics.BingBongUnheldFor > 10 && BingBongMechanics.BingBongUnheldFor <= 15) {
                if (SinceLastShake > 1 / 60) {
                    SinceLastShake = 0;
                    GamefeelHandler.instance.AddPerlinShake(0.14f,0.15f,4f);
                }
            }

            if (BingBongMechanics.BingBongUnheldFor < 15) {
                continue;
            }
            character.refs.afflictions.AddStatus(CharacterAfflictions.STATUSTYPE.Crab, 0.05f * Time.deltaTime, false);
            break;
        }
    }
}
