using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class FocusMana : Cards {

    public int manaGained;

    public FocusMana(int manaGained, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.manaGained = manaGained;
    }
    
    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        int currentTurn = StageManager.instance.currentTurn;
        Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.eventManager;
        AbstractEvent[] newManaEvent = {new GainManaEvent(1, manaGained, enemyIndex)};
        AbstractEvent[] newResetEvent = {new GainManaEvent(1, 0 - manaGained, enemyIndex)};
        if (eventManager.ContainsKey(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newManaEvent).ToArray();
        } else {
            eventManager.Add(currentTurn + 1, newManaEvent);
        }
        if (eventManager.ContainsKey(currentTurn + 2)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 2];
            eventManager[currentTurn + 2] = currEvent.Concat(newResetEvent).ToArray();
        } else {
            eventManager.Add(currentTurn + 2, newResetEvent);
        }
    }
    
}
