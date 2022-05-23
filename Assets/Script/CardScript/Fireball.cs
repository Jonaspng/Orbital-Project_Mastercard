using UnityEngine;

public class Fireball : Cards {

    public int damage;

    public Fireball(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }
    
    public override void OnDrop(int enemyIndex) {
        if (StageManager.instance.manaCount - this.manaCost >= 0) {
            StageManager.instance.playerMove(this, enemyIndex);
            GameObject.Destroy(this.transform.gameObject);
        } 
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        enemies[enemyIndex].ReceiveFireballDamage(player.GetFullDamage(damage), enemyIndex);
    }
}
