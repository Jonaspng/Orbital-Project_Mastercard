using UnityEngine;

class PoisonEvent : AbstractEvent {

    public bool isPoisoned;

    public PoisonEvent(int numberOfTurns, bool isPoisoned, int enemyIndex) 
    : base(numberOfTurns, enemyIndex){
        this.numberOfTurns = numberOfTurns;
        this.isPoisoned = isPoisoned;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        enemies[this.enemyIndex].ChangeIsPoisoned(this.isPoisoned);
    }

}