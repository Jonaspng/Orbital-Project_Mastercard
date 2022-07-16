using UnityEngine;

public class CatArmour : Enemy {
    
    private void Awake() {
        SetBaseAttack(2);
    }

    public override void EnemyMove(Player player, Enemy[] enemies, int index) {
        if (!this.isImmobilised) {
            if (this.moveNumber == 1) {
                this.GetAnimator().SetTrigger("Attack");
                this.PlayAttackSound();
                player.receiveDamage(this, this.GetFullDamage(), index);
            } else if (this.moveNumber == 2) {
                SetBaseShield(GetBaseShield() + 8);
                this.PlayShieldSound();
                this.gameObject.GetComponentInParent<BattleHUD>().RenderEnemyShieldIcon(index);
            } else {
                this.PlayShieldSound();
                foreach (Enemy enemy in enemies) {
                    if (enemy != null) {
                        enemy.SetBaseShield(enemy.GetBaseShield() + 6);
                        enemy.gameObject.GetComponentInParent<BattleHUD>().RenderEnemyShieldIcon(enemy.enemyIndex);
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
            this.gameObject.GetComponentInParent<BattleHUD>().RenderShieldAllIndicator();
        }
    } 
       

}
