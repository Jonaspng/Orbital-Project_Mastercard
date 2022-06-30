using UnityEngine;
class PlayerPoisonDamageEvent : AbstractEvent {

    public int damagePerTurn;

    public PlayerPoisonDamageEvent(int damagePerTurn, int numberOfTurns, int enemyIndex)
    : base(numberOfTurns, enemyIndex) {
        this.damagePerTurn = damagePerTurn;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        player.animator.SetTrigger("Poisoned");
        player.receivePoisonDamage(damagePerTurn);
    }



}