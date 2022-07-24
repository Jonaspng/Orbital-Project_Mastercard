using UnityEngine;

class BrokenEnemyEvent : AbstractEvent {

    // Event that makes enemy broken
    [SerializeField] private bool isBroken;

    public BrokenEnemyEvent(bool isBroken, int enemyIndex) 
    : base(enemyIndex){
        this.isBroken = isBroken;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        if (enemies[this.GetEnemyIndex()] != null) {
            enemies[this.GetEnemyIndex()].SetBrokenStatus(this.isBroken);
            enemies[this.GetEnemyIndex()].GetComponentInParent<BattleHUD>().RemoveBrokenIcon();
        }
        
    }

  


}