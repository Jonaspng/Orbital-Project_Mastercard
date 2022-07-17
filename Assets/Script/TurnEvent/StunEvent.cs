using UnityEngine;

class StunEvent : AbstractEvent {

    [SerializeField] private bool isImmobilised;

    public StunEvent(bool isImmobilised, int enemyIndex) 
    : base(enemyIndex){
        this.isImmobilised = isImmobilised;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        if (enemies[this.GetEnemyIndex()] != null) {
            enemies[this.GetEnemyIndex()].ChangeIsImmobilised(this.isImmobilised);
            enemies[this.GetEnemyIndex()].GetComponentInParent<BattleHUD>().RemoveStunIcon();
        }
    }   


}