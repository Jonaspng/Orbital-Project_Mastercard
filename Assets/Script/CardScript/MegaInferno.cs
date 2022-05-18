using UnityEngine;

public class MegaInferno : Cards {

    public int damage;

    public MegaInferno(int damage, int turns, int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }
    
    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        foreach(Enemy enemy in enemies) {
            enemy.receiveDamage(this.damage);
        }
    }
}
