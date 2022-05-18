using UnityEngine;

public class ScatterShot : Cards {

    public int damage;

    public ScatterShot(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }


    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyindex) {
        foreach (Enemy enemy in enemies) {
            Archer archer = (Archer) player;
            enemy.ReceiveArrowDamage(archer, player.GetFullDamage(damage));
        }
        
    }
}
