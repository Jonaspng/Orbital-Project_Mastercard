using UnityEngine;
using System.Linq;
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

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        
        enemies[enemyIndex].receiveDamage(player.GetFullDamage(8), enemyIndex);
        player.animator.SetTrigger("Attack");

        if (enemies[enemyIndex] != null) {
            enemies[enemyIndex].ChangeIsPoisoned(true);
            enemies[enemyIndex].GetComponentInParent<BattleHUD>().RenderEnemyPoisonIcon();
        }

        int currentTurn = StageManager.instance.currentTurn;
        Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.enemyEventManager;
        Dictionary<int, AbstractEvent[]> eventManager2 = StageManager.instance.playerEventManager;
        AbstractEvent[] newEvent = {new OvertimeDamageEvent(3, 2, enemyIndex)};
        AbstractEvent[] resetEvent = {new PoisonEvent(1, false, enemyIndex)};
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
         if (eventManager2.ContainsKey(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager2[currentTurn + 1];
            eventManager2[currentTurn + 1] = currEvent.Concat(resetEvent).ToArray();
        } else {
            eventManager2.Add(currentTurn + 1, resetEvent);
        }
        
    
    }

}