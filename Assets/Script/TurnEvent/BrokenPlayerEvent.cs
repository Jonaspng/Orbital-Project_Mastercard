using UnityEngine;

class BrokenPlayerEvent : AbstractEvent {

    // Event that makes player broken
    [SerializeField] private bool isBroken;

    public BrokenPlayerEvent(bool isBroken, int enemyIndex) 
    : base(enemyIndex){
        this.isBroken = isBroken;
    }

    public bool CheckBroken() {
        return this.isBroken;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        player.ChangeIsBroken(this.isBroken);
        if (!isBroken) {
            GameObject.Find("Player Battlestation").GetComponentInChildren<BattleHUD>().RemoveBrokenIcon();
        }
        
    }

}