using UnityEngine;


public class CatStaff : Enemy {
    public CatStaff(double attackModifier, double shieldModifier)
    : base(30, attackModifier, shieldModifier) {
        
    }

    public void OnTriggerEnter2D(Collider2D other) {
         Debug.Log("test");
     }

    public override void EnemyMove(Player player, Enemy[] enemies, int index) {
        int moveNumber = Random.Range(1, 4);
        int numberOfEnemies = enemies.Length;
        if (!this.isImmobilised) {
            if (moveNumber == 1) {
                print("Enemy Attacks");
                this.animator.SetTrigger("Attack");
                player.receiveDamage(this, this.GetFullDamage(8), index);      
            } else if (moveNumber == 2) {
                print("Enemy Defends");
                this.AddBaseShield(6);
                this.gameObject.GetComponentInParent<BattleHUD>().RenderEnemyShieldIcon(6, index);
            } else {
                foreach(Enemy enemy in enemies) {
                    this.changeAttackModifier(1.25);
                }
            }
        }
        
    }
}
