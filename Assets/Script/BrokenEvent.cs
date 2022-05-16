using UnityEngine;

class BrokenEvent : AbstractEvent {

    public bool isBroken;

    public BrokenEvent(int numberOfTurns, bool isBroken, int enemyIndex) 
    : base(numberOfTurns, enemyIndex){
        this.numberOfTurns = numberOfTurns;
        this.isBroken = isBroken;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        enemies[this.enemyIndex].ChangeIsBroken(this.isBroken);
    }

    public override void ExecuteEnemyEvent(Player player, Enemy enemy) {
        player.ChangeIsBroken(this.isBroken);
    }



    


}