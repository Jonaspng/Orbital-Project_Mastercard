using UnityEngine;

public class SniperShot : Cards {

    public int damage;

    public SniperShot(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }
    
    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        Archer archer = (Archer) player;
        enemies[enemyIndex].ReceiveArrowDamage(archer, player.GetFullDamage(damage));
    }
}
