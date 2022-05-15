using UnityEngine;

class StunEvent : AbstractEvent {

    public bool isImmobilised;

    public StunEvent(int numberOfTurns, bool isImmobilised) : base(numberOfTurns){
        this.numberOfTurns = numberOfTurns;
        this.isImmobilised = isImmobilised;
    }

    public override void executeEvent(Player player, Enemy enemy) {
        enemy.ChangeIsImmobilised(this.isImmobilised);
    }


    


}