using UnityEngine;

class EnemyDamageEvent : AbstractEvent {
    
    //affects Enemy
    [SerializeField] private int damage;

    public EnemyDamageEvent(int damage, int enemyIndex) 
    : base(enemyIndex){
        this.damage = damage;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        if (enemies[this.GetEnemyIndex()] != null) {
            enemies[this.GetEnemyIndex()].SetBaseAttack(this.damage);
        }
    }
   


}