using System.Linq;
using System.Collections.Generic;

public class CatSword : Enemy {
    
    private void Awake() {
        SetBaseAttack(3);
    }

    public bool CheckForBrokenResetEvent(AbstractEvent element) {
        if (element is BrokenPlayerEvent) {
            BrokenPlayerEvent temp = (BrokenPlayerEvent) element;
            if (!temp.CheckBroken()) {
                return true;
            }
        }
        return false;
    }

    public override void EnemyMove(Player player, Enemy[] enemies, int index) {
        if (!this.CheckImmobilised()) {
            if (this.GetMoveNumber() == 1) {
                print("Cat attack with " + this.GetFullDamage());
                this.GetAnimator().SetTrigger("Attack");
                this.PlayAttackSound();
                player.receiveDamage(this, this.GetFullDamage(), index);
                
            } else if (this.GetMoveNumber() == 2) {
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
                
                int currentTurn = StageManager.GetInstance().GetCurrentTurn();

                Dictionary<int, AbstractEvent[]> eventManager = StageManager.GetInstance().GetPlayerEventManager();
                AbstractEvent[] newResetEvent = {new BrokenPlayerEvent(false, this.GetEnemyIndex())};
                AbstractEvent[] newEvent = {new BrokenPlayerEvent(true, this.GetEnemyIndex())};

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
        if (this.GetMoveNumber() == 1) {
            this.gameObject.GetComponentInParent<BattleHUD>().RenderAttackIndicator();
        } else if (this.GetMoveNumber() == 2) {
            this.gameObject.GetComponentInParent<BattleHUD>().RenderShieldIndicator();
        } else {
            this.gameObject.GetComponentInParent<BattleHUD>().RenderBrokenIndicator();
        }
    } 
       

}
