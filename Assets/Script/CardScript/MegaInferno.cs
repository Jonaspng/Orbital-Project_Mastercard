using UnityEngine;

public class MegaInferno : Cards {

    public int damage;

    public MegaInferno(int damage, int turns, int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }
    
    public void OnMouseDown() {
        int enemyIndex = 0;
        if (StageManager.instance.manaCount - this.manaCost >= 0) {
            StageManager.instance.playerMove(this, enemyIndex);
            GameObject.Destroy(this.transform.gameObject);
        } 
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        foreach(Enemy enemy in enemies) {
            enemy.receiveDamage(this.damage);
        }
    }
}
