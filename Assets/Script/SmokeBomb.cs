using UnityEngine;
using System.Linq;
using System.Collections;

public class SmokeBomb : Cards {

    public SmokeBomb(int turns, 
    int manaCost) : base(manaCost, turns) {

    }

    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        Archer archer = (Archer) player;
        int currentTurn = StageManager.instance.currentTurn;
        Hashtable eventManager = StageManager.instance.eventManager;
        AbstractEvent[] newResetEvent = {new StealthEvent(1, false, enemyIndex)};
        if (eventManager.Contains(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[]) eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newResetEvent);
        } else {
            eventManager.Add(currentTurn + 1, newResetEvent);
        }
        archer.ChangeStealthStatus(true);
    }
}
