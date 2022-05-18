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
            this.changeBaseShield(6);
        } else {
            int currentTurn = StageManager.instance.currentTurn;
            Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.eventManager;
            AbstractEvent[] newResetEvent = new AbstractEvent[numberOfEnemies];
            foreach(Enemy enemy in enemies) {
                this.changeAttackModifier(1.25);
            }
            for (int i = 0; i < numberOfEnemies; i++) {
                newResetEvent[i] = new DamageEvent(1, -5, i);
            }
            if (eventManager.ContainsKey(currentTurn + 1)) {
                AbstractEvent[] currEvent = (AbstractEvent[]) eventManager[currentTurn + 1];
                eventManager[currentTurn + 1] = currEvent.Concat(newResetEvent).ToArray();
            } else {
                eventManager.Add(currentTurn + 1, newResetEvent);
            }
                       
        }
    }
}
