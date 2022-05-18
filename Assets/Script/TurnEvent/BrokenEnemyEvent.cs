using UnityEngine;

class BrokenEnemyEvent : AbstractEvent {

    // Event that makes enemy broken
    public bool isBroken;

    public BrokenEnemyEvent(int numberOfTurns, bool isBroken, int enemyIndex) 
    : base(numberOfTurns, enemyIndex){
        this.numberOfTurns = numberOfTurns;
        this.isBroken = isBroken;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        enemies[this.enemyIndex].ChangeIsBroken(this.isBroken);
    }

  


}