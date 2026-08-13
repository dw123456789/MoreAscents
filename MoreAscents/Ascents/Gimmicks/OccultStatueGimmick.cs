using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreAscents;

public class OccultStatueGimmick : AscentGimmick {
    public override string GetTitle() {
        return "Ascent 13";
    }

    public override string GetDescription() {
        return "No more Revive Statues.";
    }

    private List<Spawner> despawnQueue;
    private readonly object despawnQueue_lockObj = new object();
    public override void RespawnChestExisted(Spawner chest) {
        if (MoreAscents.Plugin.ascent13Disabler.Value)
            return;
        lock (despawnQueue_lockObj) {
            if (despawnQueue == null) { // create a coroutine to remove the statues later on.
                                        // We need to do this as, apparently, bingbong doesnt spawn if the first statue is removed immediately??
                despawnQueue = new List<Spawner>();
                Plugin.Instance.StartCoroutine(DespawnStatusDelayed());
            }
            despawnQueue.Add(chest);
        }
    }

    private IEnumerator DespawnStatusDelayed() {
        yield return new WaitForSeconds(45); // should be more than enough time for bingbong to spawn while also not revealing the statues to the player
        lock (despawnQueue_lockObj) {
            if (despawnQueue != null) {
                foreach (var item in despawnQueue) {
                    item.gameObject.SetActive(false);
                }
                despawnQueue = null;
            }
        }
    }
}