using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCat : Enemy {

    public BossCat(double attackModifier, double shieldModifier)
    : base(110, attackModifier, shieldModifier) {
        
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
                this.gameObject.GetComponentInParent<BattleHUD>().RenderEnemyShieldIcon(index);
            } else {
                print("Spawning Enemy");
                if (StageManager.instance.enemyCount == 3) {
                    GameManager.instance.SpawnEnemies();
                } else {
                    EnemyMove(player, enemies, index);
                }
            }
        }
    }   
}
