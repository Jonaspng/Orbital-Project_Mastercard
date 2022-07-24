using UnityEngine;
class OvertimeDamageEvent : AbstractEvent {

    [SerializeField] private int damagePerTurn;

    public OvertimeDamageEvent(int damagePerTurn, int enemyIndex)
    : base(enemyIndex) {
        this.damagePerTurn = damagePerTurn;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        if (enemies[this.GetEnemyIndex()] != null) {
            enemies[this.GetEnemyIndex()].GetAnimator().SetTrigger("Poisoned");
            enemies[this.GetEnemyIndex()].receiveOverTimeDamage(damagePerTurn, this.GetEnemyIndex());              
        }
    }

}