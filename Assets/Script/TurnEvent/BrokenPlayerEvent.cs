using UnityEngine;

class BrokenPlayerEvent : AbstractEvent {

    // Event that makes player broken
    public bool isBroken;

    public BrokenPlayerEvent(int numberOfTurns, bool isBroken, int enemyIndex) 
    : base(numberOfTurns, enemyIndex){
        this.numberOfTurns = numberOfTurns;
        this.isBroken = isBroken;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        player.ChangeIsBroken(this.isBroken);
        GameObject.Find("PlayerHUD").GetComponent<BattleHUD>().RemoveBrokenIcon();
    }

}