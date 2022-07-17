using UnityEngine;

public class BossCat : Enemy {

    private void Awake() {
        SetBaseAttack(8);
    }

    public override void EnemyMove(Player player, Enemy[] enemies, int index) {
        int numberOfEnemies = enemies.Length;
        if (!this.CheckImmobilised()) {
            if (this.GetMoveNumber() == 1) {
                print("Enemy Attacks");
                this.GetAnimator().SetTrigger("Attack");
                this.PlayAttackSound();
                player.receiveDamage(this, this.GetFullDamage(), index);      
            } else if (this.GetMoveNumber() == 2) {
                print("Enemy Defends");
                SetBaseShield(GetBaseShield() + 6);
                this.PlayShieldSound();
                this.gameObject.GetComponentInParent<BattleHUD>().RenderEnemyShieldIcon(index);
            } else {
                print("Spawning Enemy");
                GameManager.GetInstance().SpawnEnemies();
            }
        }
    }

    public override void EnemyMoveNumberGenerator() {
        this.SetMoveNumber(UnityEngine.Random.Range(1, 4));
        if (StageManager.GetInstance().GetEnemyCount() == 4 && this.GetMoveNumber() == 3) {
            EnemyMoveNumberGenerator();
        }

    }

    public override void RenderWarningIndicator() {
        if (this.GetMoveNumber() == 1) {
            this.gameObject.GetComponentInParent<BattleHUD>().RenderAttackIndicator();
        } else if (this.GetMoveNumber() == 2) {
            this.gameObject.GetComponentInParent<BattleHUD>().RenderShieldIndicator();
        } else {
            this.gameObject.GetComponentInParent<BattleHUD>().RenderSummonIndicator();
        }
    } 
}
