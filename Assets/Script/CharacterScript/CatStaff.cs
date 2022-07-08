using UnityEngine;


public class CatStaff : Enemy {
    public CatStaff(double attackModifier, double shieldModifier)
    : base(30, attackModifier, shieldModifier) {
    }

    private void Awake() {
        this.baseAttack = 6;
    }

    public override void EnemyMove(Player player, Enemy[] enemies, int index) {
        int numberOfEnemies = enemies.Length;
        if (!this.isImmobilised) {
            if (this.moveNumber == 1) {
                print("Enemy Attacks");
                this.animator.SetTrigger("Attack");
                player.receiveDamage(this, this.GetFullDamage(), index);      
            } else if (this.moveNumber == 2) {
                print("Enemy Defends");
                this.AddBaseShield(6);
                this.gameObject.GetComponentInParent<BattleHUD>().RenderEnemyShieldIcon(index);
            } else {
                foreach(Enemy enemy in enemies) {
                    if (enemy != null) {
                        enemy.gameObject.GetComponentInParent<BattleHUD>().RenderAttackUpIcon();
                        enemy.changeAttackModifier(1.25);
                    }
                }
            }
        }
    }

    public override void RenderWarningIndicator() {
        if (this.moveNumber == 1) {
            this.gameObject.GetComponentInParent<BattleHUD>().RenderAttackIndicator();
        } else if (this.moveNumber == 2) {
            this.gameObject.GetComponentInParent<BattleHUD>().RenderShieldIndicator();
        } else {
            this.gameObject.GetComponentInParent<BattleHUD>().RenderAttackUpAllIndicator();
        }
    } 
}
