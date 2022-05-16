using UnityEngine;
using System.Linq;
using System.Collections;

public class CatSword : Enemy {
    
    public CatSword (double attackModifier, double shieldModifier) 
    : base(30, attackModifier, shieldModifier) { 
        //empty
    }

    public override void EnemyMove(Player player, Enemy[] enemies) {
        int moveNumber = Random.Range(1, 4);
        if (moveNumber == 1) {
            player.receiveDamage(this, this.GetFullDamage(6));
        } else if (moveNumber == 2) {
            this.changeBaseShield(6);
        } else {
            player.receiveDamage(this, this.GetFullDamage(6));
            
            int currentTurn = StageManager.instance.currentTurn;
            Hashtable eventManager = StageManager.instance.eventManager;
            AbstractEvent[] newEvent = {new BrokenEvent(1, true, -1)};
            AbstractEvent[] newResetEvent = {new BrokenEvent(1, false, -1)};
            if (eventManager.Contains(currentTurn + 1)) {
                AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
                eventManager[currentTurn + 1] = currEvent.Concat(newEvent);
            } else {
                eventManager.Add(currentTurn + 1, newEvent);
            }
            if (eventManager.Contains(currentTurn + 2)) {
                AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
                eventManager[currentTurn + 2] = currEvent.Concat(newResetEvent);
            } else {
                eventManager.Add(currentTurn + 2, newResetEvent);
            }
        }
    }
       

}
