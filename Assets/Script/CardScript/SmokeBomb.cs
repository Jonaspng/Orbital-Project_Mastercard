using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SmokeBomb : Cards {

    public SmokeBomb(int turns, 
    int manaCost) : base(manaCost, turns) {

    }

    public override void OnDrop(int enemyIndex) {
        if (StageManager.instance.manaCount - this.manaCost >= 0) {
            StageManager.instance.playerMove(this, enemyIndex);
            GameObject.Destroy(this.transform.gameObject);
        }
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        Archer archer = (Archer) player;
        int currentTurn = StageManager.instance.currentTurn;
        Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.eventManager;
        AbstractEvent[] newResetEvent = {new StealthEvent(1, false, enemyIndex)};
        if (eventManager.ContainsKey(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[]) eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newResetEvent).ToArray();
        } else {
            eventManager.Add(currentTurn + 1, newResetEvent);
        }
        archer.ChangeStealthStatus(true);
    }
}
