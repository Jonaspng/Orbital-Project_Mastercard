using UnityEngine;

public class MultipleShots : Cards {
    public int shotCount = Random.Range(2, 6);
    public int damage;

    public MultipleShots(int shotCount, int damage, int turns, int manaCost) : base(manaCost, turns) {
        this.shotCount = shotCount;
        this.damage = damage;
    }

    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);
    }
    
    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        Archer archer = (Archer) player;
        for (int i = 0; i < shotCount; i++) {
            enemies[enemyIndex].ReceiveArrowDamage(archer, player.GetFullDamage(damage));
        }      
    }
}
