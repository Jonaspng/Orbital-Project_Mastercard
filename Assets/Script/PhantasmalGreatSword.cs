using UnityEngine;

public class PhantasmalGreatSword : Cards {

    public int damage;

    public PhantasmalGreatSword(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }

    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        enemies[enemyIndex].receiveDamage(player.GetFullDamage(this.damage));
    }
}
