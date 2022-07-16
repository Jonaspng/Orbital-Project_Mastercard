using System.Linq;
using System.Collections.Generic;

public class CatSword : Enemy {
    
    private void Awake() {
        SetBaseAttack(3);
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
        if (!this.isImmobilised) {
            if (this.moveNumber == 1) {
                print("Cat attack with " + this.GetFullDamage());
                this.GetAnimator().SetTrigger("Attack");
                this.PlayAttackSound();
                player.receiveDamage(this, this.GetFullDamage(), index);
                
            } else if (this.moveNumber == 2) {
                print("applied shield");
                SetBaseShield(GetBaseShield() + 6);
                this.PlayShieldSound();
                this.gameObject.GetComponentInParent<BattleHUD>().RenderEnemyShieldIcon(index);
            } else {
                print("Cat attack with " + this.GetFullDamage() + " broken");
                this.GetAnimator().SetTrigger("Attack");
                this.PlayAttackSound();
                player.receiveDamage(this, this.GetFullDamage(), index);
                player.RenderBrokenIndicator();
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

    public override void RenderWarningIndicator() {
        if (this.moveNumber == 1) {
            this.gameObject.GetComponentInParent<BattleHUD>().RenderAttackIndicator();
        } else if (this.moveNumber == 2) {
            this.gameObject.GetComponentInParent<BattleHUD>().RenderShieldIndicator();
        } else {
            this.gameObject.GetComponentInParent<BattleHUD>().RenderBrokenIndicator();
        }
    } 
       

}
