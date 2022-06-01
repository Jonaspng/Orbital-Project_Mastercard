using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class ThunderboltArrow : Cards {

    public int damage;

    public ThunderboltArrow(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }

    public override void OnDrop(int enemyIndex) {
        if (StageManager.instance.manaCount - this.manaCost >= 0) {
            StageManager.instance.playerMove(this, enemyIndex);
            GameObject.Destroy(this.transform.gameObject);
        } 
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        Archer archer = (Archer) player;
        
        enemies[enemyIndex].ReceiveArrowDamage(archer, player.GetFullDamage(damage), enemyIndex);
        player.animator.SetTrigger("Attack");

        int currentTurn = StageManager.instance.currentTurn;
        Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.enemyEventManager;
        AbstractEvent[] newResetEvent = {new BrokenEnemyEvent(1, false, enemyIndex)};

        if (eventManager.ContainsKey(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newResetEvent).ToArray();
        } else {
            eventManager.Add(currentTurn + 1, newResetEvent);
        }

        if (enemies[enemyIndex] != null) {
            enemies[enemyIndex].ChangeIsBroken(true);
        }    
    }
}
