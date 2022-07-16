using UnityEngine;

class EnemyDamageEvent : AbstractEvent {
    
    //affects Enemy
    public int damage;

    public EnemyDamageEvent(int numberOfTurns, int damage, int enemyIndex) 
    : base(numberOfTurns, enemyIndex){
        this.numberOfTurns = numberOfTurns;
        this.damage = damage;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        if (enemies[this.enemyIndex] != null) {
            enemies[enemyIndex].SetBaseAttack(this.damage);
        }
    }
   


}