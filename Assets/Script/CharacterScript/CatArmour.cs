using UnityEngine;

public class CatArmour : Enemy {
    
    public CatArmour (double attackModifier, double shieldModifier) 
    : base(30, attackModifier, shieldModifier) { 
        //empty
    }

    public override void EnemyMove(Player player, Enemy[] enemies, int index) {
        int moveNumber = Random.Range(1, 4);
        if (moveNumber == 1) {
            player.receiveDamage(this, this.GetFullDamage(6), index);
        } else if (moveNumber == 2) {
            this.AddBaseShield(6);
            this.gameObject.GetComponentInParent<BattleHUD>().RenderEnemyShieldIcon(6, index);
        } else {
            foreach (Enemy enemy in enemies) {
                enemy.AddBaseShield(6);
                enemy.gameObject.GetComponentInParent<BattleHUD>().RenderEnemyShieldIcon(6, 0);
            }
            
        }

    }
       

}
