using UnityEngine;
using System.Linq;
using System.Collections;

public class ThunderboltArrow : Cards {

    public int damage;

    public ThunderboltArrow(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }

    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        Archer archer = (Archer) player;
        enemies[enemyIndex].ReceiveArrowDamage(archer, player.GetFullDamage(damage));

        // The part that applies broken. Implementation not finalised. 
        int currentTurn = StageManager.instance.currentTurn;
        Hashtable eventManager = StageManager.instance.eventManager;
        AbstractEvent[] newBrokenEvent = {new BrokenEvent(1, true, enemyIndex)};
        AbstractEvent[] newResetEvent = {new BrokenEvent(1, false, enemyIndex)};
        if (eventManager.Contains(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[]) eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newBrokenEvent);
        } else {
            eventManager.Add(currentTurn + 1, newBrokenEvent);
        }
        if (eventManager.Contains(currentTurn + 2)) {
            AbstractEvent[] currEvent = (AbstractEvent[]) eventManager[currentTurn + 1];
            eventManager[currentTurn + 2] = currEvent.Concat(newResetEvent);
        } else {
            eventManager.Add(currentTurn + 2, newResetEvent);
        }
    }
}
