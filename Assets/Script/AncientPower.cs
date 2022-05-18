using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class AncientPower : Cards {

    public int damage;

    public AncientPower(int damage, int turns, int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }

    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);

    }
public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        int currentTurn = StageManager.instance.currentTurn;
        Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.eventManager;
        AbstractEvent[] newAddEvent = {new PlayerDamageEvent(1, 5, enemyIndex)};
        AbstractEvent[] newResetEvent = {new PlayerDamageEvent(1, -5, enemyIndex)};
        if (eventManager.ContainsKey(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[]) eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newAddEvent).ToArray();
        } else {
            eventManager.Add(currentTurn + 1, newAddEvent);
        }
        if (eventManager.ContainsKey(currentTurn + 2)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 2];
            eventManager[currentTurn + 2] = currEvent.Concat(newResetEvent).ToArray();
        } else {
            eventManager.Add(currentTurn + 2, newResetEvent);
        }
        player.changeBaseAttack(5);
        
    }
    
}
