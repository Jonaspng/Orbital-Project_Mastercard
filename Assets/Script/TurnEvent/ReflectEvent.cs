using UnityEngine;
class ReflectEvent : AbstractEvent {
    public bool isReflected;
    public ReflectEvent(int numberOfTurns, bool isReflected, int enemyIndex) 
    : base(numberOfTurns, enemyIndex) {
        this.isReflected = isReflected;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        Mage mage = (Mage) player;
        mage.ChangeReflectStatus(isReflected);
        GameObject.Find("PlayerHUD").GetComponent<BattleHUD>().RemoveReflectIcon();
    }
}