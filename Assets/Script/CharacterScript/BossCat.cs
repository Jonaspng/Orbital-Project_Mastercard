using UnityEngine;

public class BossCat : Enemy {

    public BossCat(double attackModifier, double shieldModifier)
    : base(110, attackModifier, shieldModifier) {

    }

    private void Awake() {
        this.baseAttack = 8;
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
                print("Spawning Enemy");
                GameManager.instance.SpawnEnemies();
            }
        }
    }

    public override void EnemyMoveNumberGenerator() {
        this.moveNumber =  UnityEngine.Random.Range(1, 4);
        if (StageManager.instance.enemyCount == 4 && this.moveNumber == 3) {
            EnemyMoveNumberGenerator();
        }

    }

    public override void RenderWarningIndicator() {
        if (this.moveNumber == 1) {
            this.gameObject.GetComponentInParent<BattleHUD>().RenderAttackIndicator();
        } else if (this.moveNumber == 2) {
            this.gameObject.GetComponentInParent<BattleHUD>().RenderShieldIndicator();
        } else {
            this.gameObject.GetComponentInParent<BattleHUD>().RenderSummonIndicator();
        }
    } 
}
