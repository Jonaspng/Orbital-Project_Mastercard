using UnityEngine;

class PlayerDamageEvent : AbstractEvent {

    // Affects player
    [SerializeField] private int damage;

    public PlayerDamageEvent(int damage, int enemyIndex) 
    : base(enemyIndex){
        this.damage = damage;
    }

    public override void executeEvent(Player player, Enemy[] enemies) {
        player.SetBaseAttack(player.GetBaseAttack() + this.damage);

        if (this.damage > 0) {
            GameObject.Find("Player Battlestation").GetComponentInChildren<BattleHUD>().RenderAttackUpIcon();
        } else {
            GameObject.Find("Player Battlestation").GetComponentInChildren<BattleHUD>().RemoveAttackUpIcon();
        }
    }

}