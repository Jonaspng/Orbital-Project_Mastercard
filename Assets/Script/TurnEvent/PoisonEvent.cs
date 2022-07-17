using UnityEngine;

class PoisonEvent : AbstractEvent {

    [SerializeField] private bool isPoisoned;

    public PoisonEvent(bool isPoisoned, int enemyIndex) 
    : base(enemyIndex){
        this.isPoisoned = isPoisoned;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        if (enemies[this.GetEnemyIndex()] != null) {
            enemies[this.GetEnemyIndex()].ChangeIsPoisoned(this.isPoisoned);
            enemies[this.GetEnemyIndex()].GetComponentInParent<BattleHUD>().RemovePoisonIcon();
 
        }
    }

}