using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

class FireBlast : Cards {

    public int damage;

    public FireBlast(int manaCost, int damage, int turns) 
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
        int currentTurn = StageManager.instance.currentTurn;

        Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.enemyEventManager;
        AbstractEvent[] newEvent = {new OvertimeDamageEvent(3, 2, enemyIndex)};
        AbstractEvent[] resetEvent = {new BurnEvent(1, false, enemyIndex)};

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
        
        player.animator.SetTrigger("Attack");
        enemies[enemyIndex].receiveDamage(this.damage, enemyIndex);
        
        if (enemies[enemyIndex] != null) {
            enemies[enemyIndex].ChangeIsBurned(true);
            enemies[enemyIndex].GetComponentInParent<BattleHUD>().RenderEnemyBurnIcon();
        }

        
    
    }

}