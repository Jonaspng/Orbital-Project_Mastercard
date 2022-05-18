using UnityEngine;
using System.Linq;
using System.Collections;

class ThunderWave : Cards {

    public ThunderWave(int manaCost, int turns) 
    : base(manaCost, turns){

    }

    // Update is called once per frame
    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyindex) {
        int currentTurn = StageManager.instance.currentTurn;
        Hashtable eventManager = StageManager.instance.eventManager;
        AbstractEvent[] newResetEvent = {new StunEvent(1, false, 0)};
        if (eventManager.Contains(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newResetEvent);
        } else {
            eventManager.Add(currentTurn + 1, newResetEvent);
        }
        enemies[enemyindex].ChangeIsImmobilised(true);
    }

}