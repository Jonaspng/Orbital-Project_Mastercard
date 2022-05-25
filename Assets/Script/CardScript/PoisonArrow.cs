using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

class PoisonArrow : Cards {

    public int damage;

    public PoisonArrow(int damage, int manaCost, int turns) 
    : base(manaCost, turns){
        this.damage = damage;
    }


    public override void OnDrop(int enemyIndex) {
        if (StageManager.instance.manaCount - this.manaCost >= 0) {
            StageManager.instance.playerMove(this, enemyIndex);
            GameObject.Destroy(this.transform.gameObject);
        }
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyindex) {
        enemies[enemyindex].receiveDamage(player.GetFullDamage(8), enemyindex);

        // The part that applies poison. Implementation to be improved. 
         int currentTurn = StageManager.instance.currentTurn;
        Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.enemyEventManager;
        AbstractEvent[] newEvent = {new OvertimeDamageEvent(3, 2, enemyindex)};
        AbstractEvent[] resetEvent = {new PoisonEvent(1, false, enemyindex)};
        if (eventManager.ContainsKey(currentTurn)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn];
            eventManager[currentTurn] = currEvent.Concat(newEvent).ToArray();
        } else {
            eventManager.Add(currentTurn, newEvent);
        }
        if (eventManager.ContainsKey(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newEvent).ToArray();
        } else {
            eventManager.Add(currentTurn + 1, newEvent);
        }
         if (eventManager.ContainsKey(currentTurn + 2)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 2];
            eventManager[currentTurn + 2] = currEvent.Concat(resetEvent).ToArray();
        } else {
            eventManager.Add(currentTurn + 2, resetEvent);
        }
        enemies[enemyindex].ChangeIsPoisoned(true);
    
    }

}