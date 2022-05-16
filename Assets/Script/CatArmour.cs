using UnityEngine;

public class CatArmour : Enemy {
    
    public CatArmour (double attackModifier, double shieldModifier) 
    : base(30, attackModifier, shieldModifier) { 
        //empty
    }

    public override void EnemyMove(Player player, Enemy[] enemies) {
        int moveNumber = Random.Range(1, 4);
        if (moveNumber == 1) {
            player.receiveDamage(this, this.GetFullDamage(6));
        } else if (moveNumber == 2) {
            this.changeBaseShield(6);
        } else {
            foreach (Enemy enemy in enemies) {
                enemy.changeBaseShield(6);
            }
            
        }

    }
       

}
