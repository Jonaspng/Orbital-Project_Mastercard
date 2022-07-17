using UnityEngine;
class PlayerPoisonDamageEvent : AbstractEvent {

    [SerializeField] private int damagePerTurn;

    public PlayerPoisonDamageEvent(int damagePerTurn, int enemyIndex)
    : base(enemyIndex) {
        this.damagePerTurn = damagePerTurn;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        player.GetAnimator().SetTrigger("Poisoned");
        player.receivePoisonDamage(damagePerTurn);
    }



}