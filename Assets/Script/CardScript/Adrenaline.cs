using UnityEngine;

public class Adrenaline : Cards {

    public double damageModifier;

    public Adrenaline(double damageModifier, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damageModifier = damageModifier;
    }

    public override void OnDrop(int enemyIndex) {
        if (StageManager.instance.manaCount - this.manaCost >= 0) {
            StageManager.instance.playerMove(this, enemyIndex);
            GameObject.Destroy(this.transform.gameObject);
        } 
        
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        player.AddAttackModifier(damageModifier);
        GameObject.Find("PlayerHUD").GetComponent<BattleHUD>().RenderAttackUpIcon();
    }
}
