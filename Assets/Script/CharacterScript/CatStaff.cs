using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CatStaff : Enemy {
    public CatStaff(double attackModifier, double shieldModifier)
    : base(30, attackModifier, shieldModifier) {
        
    }

    public override void EnemyMove(Player player, Enemy[] enemies) {
        int moveNumber = Random.Range(1, 4);
        int numberOfEnemies = enemies.Length;
        if (moveNumber == 1) {
            print("Enemy Attacks");
            player.receiveDamage(this, this.GetFullDamage(6));
        } else if (moveNumber == 2) {
            print("Enemy Defends");
            this.AddBaseShield(6);
        } else {
            foreach(Enemy enemy in enemies) {
                this.changeAttackModifier(1.25);
            }
        }
    }
}