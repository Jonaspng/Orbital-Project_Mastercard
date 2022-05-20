using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class ArcaneBolt : Cards {

    public int damage;


    public ArcaneBolt(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }

    public void OnMouseDown() {

        int enemyIndex = 0;
        if (StageManager.instance.manaCount - this.manaCost >= 0) {
            StageManager.instance.playerMove(this, enemyIndex);
            GameObject.Destroy(this.transform.gameObject);
        } 
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        int currentTurn = StageManager.instance.currentTurn;
        Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.eventManager;
        AbstractEvent[] newEvent = {new BrokenEnemyEvent(1, true, enemyIndex)};
        AbstractEvent[] newResetEvent = {new BrokenEnemyEvent(1, false, enemyIndex)};
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
        enemies[enemyIndex].receiveDamage(player.GetFullDamage(this.damage), enemyIndex);
    }

}
