using UnityEngine;

class AddDamageEvent : AbstractEvent {

    public int damage;

    public AddDamageEvent(int numberOfTurns, int damage) : base(numberOfTurns){
        this.numberOfTurns = numberOfTurns;
        this.damage = damage;
    }

    public override void executeEvent(Player player, Enemy enemy) {
        player.changeBaseAttack(this.damage);
    }


    


}