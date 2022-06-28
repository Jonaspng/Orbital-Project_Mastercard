using UnityEngine;

class PlayerPoisonEvent : AbstractEvent {

    public bool isPoisoned;

    public PlayerPoisonEvent(int numberOfTurns, bool isPoisoned, int enemyIndex) 
    : base(numberOfTurns, enemyIndex){
        this.numberOfTurns = numberOfTurns;
        this.isPoisoned = isPoisoned;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        if (player != null) {
            player.ChangeIsPoisoned(this.isPoisoned);
            GameObject.Find("Player Battlestation").GetComponentInChildren<BattleHUD>().RemovePoisonIcon();
        }
    }

}