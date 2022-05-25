using UnityEngine;
class OvertimeDamageEvent : AbstractEvent {

    public int damagePerTurn;

    public OvertimeDamageEvent(int damagePerTurn, int numberOfTurns, int enemyIndex)
    : base(numberOfTurns, enemyIndex) {
        this.damagePerTurn = damagePerTurn;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        if (enemies[this.enemyIndex] != null) {
            enemies[enemyIndex].receiveDamage(damagePerTurn, enemyIndex);
        }
    }

}