using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class CatSword : Enemy {
    
    public CatSword (double attackModifier, double shieldModifier) 
    : base(30, attackModifier, shieldModifier) { 
        //empty
    }

    public override void EnemyMove(Player player, Enemy[] enemies, int index) {
        int moveNumber = Random.Range(1, 4);
        if (!this.isImmobilised) {
            if (moveNumber == 1) {
                print("Cat attack with " + this.GetFullDamage(6));
                this.animator.SetTrigger("Attack");
                player.receiveDamage(this, this.GetFullDamage(6), index);
            } else if (moveNumber == 2) {
                print("applied shield");
                this.AddBaseShield(6);
                this.gameObject.GetComponentInParent<BattleHUD>().RenderEnemyShieldIcon(6, index);
            } else {
                print("Cat attack with " + this.GetFullDamage(6) + " broken");
                this.animator.SetTrigger("Attack");
                player.receiveDamage(this, this.GetFullDamage(6), index);

                player.ChangeIsBroken(true);
                
                int currentTurn = StageManager.instance.currentTurn;

                Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.enemyEventManager;
                AbstractEvent[] newResetEvent = {new BrokenPlayerEvent(1, false, -1)};
                
                if (eventManager.ContainsKey(currentTurn + 2)) {
                    AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 2];
                    eventManager[currentTurn + 2] = currEvent.Concat(newResetEvent).ToArray();
                } else {
                    eventManager.Add(currentTurn + 2, newResetEvent);
                }
            }
        }
        
    }
       

}
