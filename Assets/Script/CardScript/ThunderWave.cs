using UnityEngine;
using System.Linq;
using System.Collections.Generic;

class ThunderWave : Cards {

    public ThunderWave(int manaCost, int turns) 
    : base(manaCost, turns){

    }

    // Update is called once per frame
    public override void OnDrop(int enemyIndex) {
        if (StageManager.instance.manaCount - this.manaCost >= 0) {
            StageManager.instance.playerMove(this, enemyIndex);
            GameObject.Destroy(this.transform.gameObject);
        }
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        int currentTurn = StageManager.instance.currentTurn;
        Dictionary<int,AbstractEvent[]> eventManager = StageManager.instance.playerEventManager;
        AbstractEvent[] newResetEvent = {new StunEvent(1, false, enemyIndex)};
        if (eventManager.ContainsKey(currentTurn)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn];
            eventManager[currentTurn] = currEvent.Concat(newResetEvent).ToArray();
        } else {
            eventManager.Add(currentTurn, newResetEvent);
        }
        enemies[enemyIndex].ChangeIsImmobilised(true);
        enemies[enemyIndex].GetComponentInParent<BattleHUD>().RenderStunIcon();
    }

}