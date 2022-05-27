using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class AncientPower : Cards {

    public int damage;

    public AncientPower(int damage, int turns, int manaCost) : base(manaCost, turns) {
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
        Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.playerEventManager;
        AbstractEvent[] newAddEvent = {new PlayerDamageEvent(1, 5, enemyIndex)};
        AbstractEvent[] newResetEvent = {new PlayerDamageEvent(1, -5, enemyIndex)};
        if (eventManager.ContainsKey(currentTurn)) {
            AbstractEvent[] currEvent = (AbstractEvent[]) eventManager[currentTurn];
            eventManager[currentTurn] = currEvent.Concat(newAddEvent).ToArray();
        } else {
            eventManager.Add(currentTurn, newAddEvent);
        }
        if (eventManager.ContainsKey(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newResetEvent).ToArray();
        } else {
            eventManager.Add(currentTurn + 1, newResetEvent);
        }        
    }
    
}
