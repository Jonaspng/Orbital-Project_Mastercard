using UnityEngine;

class PoisonEvent : AbstractEvent {

    public bool isPoisoned;

    public PoisonEvent(int numberOfTurns, bool isPoisoned, int enemyIndex) 
    : base(numberOfTurns, enemyIndex){
        this.numberOfTurns = numberOfTurns;
        this.isPoisoned = isPoisoned;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        if (enemies[this.enemyIndex] != null) {
            enemies[this.enemyIndex].ChangeIsPoisoned(this.isPoisoned);
            if (!this.isPoisoned) {
                enemies[enemyIndex].GetComponentInParent<BattleHUD>().RemovePoisonIcon();
            }
        }
    }

}