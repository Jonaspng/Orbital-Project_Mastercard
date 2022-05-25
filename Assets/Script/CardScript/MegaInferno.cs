using UnityEngine;

public class MegaInferno : Cards {

    public int damage;

    public MegaInferno(int damage, int turns, int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }
    
    public override void OnDrop(int enemyIndex) {
        if (StageManager.instance.manaCount - this.manaCost >= 0) {
            StageManager.instance.playerMove(this, enemyIndex);
            GameObject.Destroy(this.transform.gameObject);
        } 
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        for (int i = 0; i < enemies.Length; i ++) {
            if (enemies[i] != null) {
                enemies[i].receiveDamage(this.damage, i);
            }         
        }
    }
}
