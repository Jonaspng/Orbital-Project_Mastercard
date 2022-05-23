using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

class PommelStrike : Cards {

    public PommelStrike(int manaCost, int turns) 
    : base(manaCost, turns){

    }

    public override void OnDrop(int enemyIndex) {
        if (StageManager.instance.manaCount - this.manaCost >= 0) {
            StageManager.instance.playerMove(this, enemyIndex);
            GameObject.Destroy(this.transform.gameObject);
        } 
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyindex) {
        int currentTurn = StageManager.instance.currentTurn;
        Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.eventManager;
        AbstractEvent[] newResetEvent = {new StunEvent(1, false, 0)};
        if (eventManager.ContainsKey(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newResetEvent).ToArray();
        } else {
            eventManager.Add(currentTurn + 1, newResetEvent);
        }
        enemies[enemyindex].ChangeIsImmobilised(true);
    }

}