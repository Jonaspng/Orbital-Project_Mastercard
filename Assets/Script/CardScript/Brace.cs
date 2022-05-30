using UnityEngine;

public class Brace : Cards {

    public int shield;

    public int damage;

    public Brace(int shield, int damage, int turns, int manaCost) : base(manaCost, turns) {
        this.shield = shield;
        this.damage = damage;
    }

    public override void OnDrop(int enemyIndex) {
        if (StageManager.instance.manaCount - this.manaCost >= 0) {
            StageManager.instance.playerMove(this, enemyIndex);
            StageManager.instance.playerHUD.RenderPlayerShieldIcon(this.shield);
            GameObject.Destroy(this.transform.gameObject);
        } 
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        player.AddBaseShield(this.shield);
        enemies[enemyIndex].receiveDamage(player.GetFullDamage(this.damage), enemyIndex);
    }
}
