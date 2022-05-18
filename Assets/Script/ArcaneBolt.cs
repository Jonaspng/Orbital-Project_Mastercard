using UnityEngine;
using System.Linq;
using System.Collections;


public class ArcaneBolt : Cards {

    public int damage;


    public ArcaneBolt(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }

    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        int currentTurn = StageManager.instance.currentTurn;
        Hashtable eventManager = StageManager.instance.eventManager;
        AbstractEvent[] newEvent = {new BrokenEvent(1, true, enemyIndex)};
        AbstractEvent[] newResetEvent = {new BrokenEvent(1, false, enemyIndex)};
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
        enemies[enemyIndex].receiveDamage(player.GetFullDamage(this.damage));
    }

}
