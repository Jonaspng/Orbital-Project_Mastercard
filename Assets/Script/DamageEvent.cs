using UnityEngine;

class DamageEvent : AbstractEvent {

    public int damage;

    public DamageEvent(int numberOfTurns, int damage) : base(numberOfTurns){
        this.numberOfTurns = numberOfTurns;
        this.damage = damage;
    }

    public override void executeEvent(Player player, Enemy enemy) {
        player.changeBaseAttack(this.damage);
    }


    


}