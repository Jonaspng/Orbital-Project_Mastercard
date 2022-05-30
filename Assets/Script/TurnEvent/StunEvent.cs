using UnityEngine;

class StunEvent : AbstractEvent {

    public bool isImmobilised;

    public StunEvent(int numberOfTurns, bool isImmobilised, int enemyIndex) 
    : base(numberOfTurns, enemyIndex){
        this.numberOfTurns = numberOfTurns;
        this.isImmobilised = isImmobilised;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        if (enemies[this.enemyIndex] != null) {
        enemies[this.enemyIndex].ChangeIsImmobilised(this.isImmobilised);
        }
    }   


}