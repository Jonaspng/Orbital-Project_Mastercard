using UnityEngine;

class BurnEvent : AbstractEvent {

    [SerializeField] private bool isBurned;

    public BurnEvent(bool isBurned, int enemyIndex) 
    : base(enemyIndex){
        this.isBurned = isBurned;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        if (enemies[this.GetEnemyIndex()] != null) {
            enemies[this.GetEnemyIndex()].ChangeIsBurned(this.isBurned);
            enemies[this.GetEnemyIndex()].GetComponentInParent<BattleHUD>().RemoveBurnIcon();
        }
    }

}