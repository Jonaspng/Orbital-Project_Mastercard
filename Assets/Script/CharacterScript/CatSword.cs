using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

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
            this.AddBaseShield(6);
        } else {
            player.receiveDamage(this, this.GetFullDamage(6));
            
            int currentTurn = StageManager.instance.currentTurn;
            Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.eventManager;
            AbstractEvent[] newEvent = {new BrokenPlayerEvent(1, true, -1)};
            AbstractEvent[] newResetEvent = {new BrokenPlayerEvent(1, false, -1)};
            if (eventManager.ContainsKey(currentTurn + 1)) {
                AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
                eventManager[currentTurn + 1] = currEvent.Concat(newEvent).ToArray();
            } else {
                eventManager.Add(currentTurn + 1, newEvent);
            }
            if (eventManager.ContainsKey(currentTurn + 2)) {
                AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
                eventManager[currentTurn + 2] = currEvent.Concat(newResetEvent).ToArray();
            } else {
                eventManager.Add(currentTurn + 2, newResetEvent);
            }
        }
    }
       

}
