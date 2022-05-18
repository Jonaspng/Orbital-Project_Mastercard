using UnityEngine;
using System.Linq;
using System.Collections;

class FireBlast : Cards {

    public FireBlast(int manaCost, int turns) 
    : base(manaCost, turns){

    }

    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyindex) {
         int currentTurn = StageManager.instance.currentTurn;
        Hashtable eventManager = StageManager.instance.eventManager;
        AbstractEvent[] newEvent = {new OvertimeDamageEvent(1, 3, enemyindex)};
        AbstractEvent[] resetEvent = {new BurnEvent(1, false, enemyindex)};
        if (eventManager.Contains(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newEvent);
        } else {
            eventManager.Add(currentTurn + 1, newEvent);
        }
        if (eventManager.Contains(currentTurn + 2)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 2];
            eventManager[currentTurn + 2] = currEvent.Concat(newEvent);
        } else {
            eventManager.Add(currentTurn + 2, newEvent);
        }
         if (eventManager.Contains(currentTurn + 3)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 3];
            eventManager[currentTurn + 2] = currEvent.Concat(resetEvent);
        } else {
            eventManager.Add(currentTurn + 2, resetEvent);
        }
        enemies[enemyindex].ChangeIsBurned(true);
    
    }

}