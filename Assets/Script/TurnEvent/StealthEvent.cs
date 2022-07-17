using UnityEngine;

class StealthEvent : AbstractEvent {

    [SerializeField] private bool isStealthed;
    public StealthEvent(bool isStealthed, int enemyIndex) 
    : base(enemyIndex) {
        this.isStealthed = isStealthed;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        Archer archer = (Archer) player;
        archer.ChangeStealthStatus(isStealthed);
        GameObject.Find("Player Battlestation").GetComponentInChildren<BattleHUD>().RemoveSmokeBombIcon();
    }

}