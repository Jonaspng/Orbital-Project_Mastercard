using UnityEngine;

class BurnEvent : AbstractEvent {

    public bool isBurned;

    public BurnEvent(int numberOfTurns, bool isBurned, int enemyIndex) 
    : base(numberOfTurns, enemyIndex){
        this.numberOfTurns = numberOfTurns;
        this.isBurned = isBurned;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        if (enemies[this.enemyIndex] != null) {
            enemies[this.enemyIndex].ChangeIsBurned(this.isBurned);
            enemies[enemyIndex].GetComponentInParent<BattleHUD>().RemoveBurnIcon();
        }
    }

}