using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class Bash : Cards {

    public int damage;

    public Bash(int damage, int turns, 
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
        int currentTurn = StageManager.instance.currentTurn;
        Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.eventManager;
                
        AbstractEvent[] newResetEvent = {new BrokenEnemyEvent(1, false, enemyIndex)};
        
        if (eventManager.ContainsKey(currentTurn + 2)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 2] = currEvent.Concat(newResetEvent).ToArray();
        } else {
            eventManager.Add(currentTurn + 2, newResetEvent);
        }
        enemies[enemyIndex].receiveDamage(player.GetFullDamage(this.damage), enemyIndex);
        enemies[enemyIndex].ChangeIsBroken(true);
    }
}
