using UnityEngine;

class DamageEvent : AbstractEvent {

    public int damage;

    public DamageEvent(int numberOfTurns, int damage, int enemyIndex) 
    : base(numberOfTurns, enemyIndex){
        this.numberOfTurns = numberOfTurns;
        this.damage = damage;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        player.changeBaseAttack(this.damage);
    }


    


}