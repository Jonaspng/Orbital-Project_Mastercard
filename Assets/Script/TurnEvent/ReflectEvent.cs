using UnityEngine;
class ReflectEvent : AbstractEvent {
    [SerializeField] private bool isReflected;
    public ReflectEvent(bool isReflected, int enemyIndex) 
    : base(enemyIndex) {
        this.isReflected = isReflected;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        Mage mage = (Mage) player;
        mage.ChangeReflectStatus(isReflected);
        GameObject.Find("Player Battlestation").GetComponentInChildren<BattleHUD>().RemoveReflectIcon();
    }
}