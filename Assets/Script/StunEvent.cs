using UnityEngine;

class StunEvent : AbstractEvent {

    public bool isImmobilised;

    public StunEvent(int numberOfTurns, bool isImmobilised, int enemyIndex) 
    : base(numberOfTurns, enemyIndex){
        this.numberOfTurns = numberOfTurns;
        this.isImmobilised = isImmobilised;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        enemies[this.enemyIndex].ChangeIsImmobilised(this.isImmobilised);
    }


    


}