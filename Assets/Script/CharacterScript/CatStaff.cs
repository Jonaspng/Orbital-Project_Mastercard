using UnityEngine;


public class CatStaff : Enemy {

    private void Awake() {
        SetBaseAttack(6);
    }

    public override void EnemyMove(Player player, Enemy[] enemies, int index) {
        int numberOfEnemies = enemies.Length;
        if (!this.isImmobilised) {
            if (this.moveNumber == 1) {
                print("Enemy Attacks");
                this.PlayAttackSound();
                this.GetAnimator().SetTrigger("Attack");
                player.receiveDamage(this, this.GetFullDamage(), index);      
            } else if (this.moveNumber == 2) {
                print("Enemy Defends");
                SetBaseShield(GetBaseShield() + 6);
                this.PlayShieldSound();
                this.gameObject.GetComponentInParent<BattleHUD>().RenderEnemyShieldIcon(index);
            } else {
                foreach(Enemy enemy in enemies) {
                    if (enemy != null) {
                        enemy.gameObject.GetComponentInParent<BattleHUD>().RenderAttackUpIcon();
                        enemy.SetAttackModifier(1.25);
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
