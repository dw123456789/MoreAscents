using System.Collections.Generic;
using UnityEngine;

namespace MoreAscents;

public class LuggageGimmick : AscentGimmick
{
    public override string GetTitle() {
        return "Ascent 11";
    }
    
    public override string GetDescription()
    {
        return "Big Luggage's have a chance to only contain one item.";
    }

    public override void SpawnerSpawnItems(Spawner spawner, ref List<Transform> spawnSpots) {
        if (spawner.gameObject.name == "LuggageBig" && Random.Range(0,2) == 0) {
            List<Transform> newList = new();
            newList.Add(spawnSpots[0]);
            spawnSpots = newList;
        }
    }
}