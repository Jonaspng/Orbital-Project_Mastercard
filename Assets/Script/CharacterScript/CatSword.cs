using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class CatSword : Enemy {
    
    public CatSword (double attackModifier, double shieldModifier) 
    : base(30, attackModifier, shieldModifier) { 
        //empty
    }

    public bool CheckForBrokenResetEvent(AbstractEvent element) {
        if (element is BrokenPlayerEvent) {
            BrokenPlayerEvent temp = (BrokenPlayerEvent) element;
            if (!temp.isBroken) {
                return true;
            }
        }
        return false;
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
                this.gameObject.GetComponentInParent<BattleHUD>().RenderEnemyShieldIcon(index);
            } else {
                print("Cat attack with " + this.GetFullDamage(6) + " broken");
                this.animator.SetTrigger("Attack");
                player.receiveDamage(this, this.GetFullDamage(6), index);

                player.ChangeIsBroken(true);
                
                int currentTurn = StageManager.instance.currentTurn;

                Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.playerEventManager;
                AbstractEvent[] newResetEvent = {new BrokenPlayerEvent(1, false, this.enemyIndex)};
                AbstractEvent[] newEvent = {new BrokenPlayerEvent(1, true, this.enemyIndex)};

                if (eventManager.ContainsKey(currentTurn)) {
                    AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn];
                    currEvent = currEvent.Where((element, index) => !CheckForBrokenResetEvent(element)).ToArray();
                    eventManager[currentTurn] = currEvent.Concat(newEvent).ToArray();
                } else {
                    eventManager.Add(currentTurn, newEvent);
                }
                
                if (eventManager.ContainsKey(currentTurn + 1)) {
                    AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
                    eventManager[currentTurn + 1] = currEvent.Concat(newResetEvent).ToArray();
                } else {
                    eventManager.Add(currentTurn + 1, newResetEvent);
                }
            }
        }
        
    }
       

}
