using UnityEngine;

class PlayerPoisonEvent : AbstractEvent {

    [SerializeField] private bool isPoisoned;

    public PlayerPoisonEvent(bool isPoisoned, int enemyIndex) 
    : base(enemyIndex){
        this.isPoisoned = isPoisoned;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        if (player != null) {
            player.ChangeIsPoisoned(this.isPoisoned);
            GameObject.Find("Player Battlestation").GetComponentInChildren<BattleHUD>().RemovePoisonIcon();
        }
    }

}