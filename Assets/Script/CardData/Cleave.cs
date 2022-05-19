using UnityEngine;

public class CleaveData : CardData {


    public int damage;

    public CleaveData(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyindex) {
        foreach (Enemy enemy in enemies) {
            enemy.receiveDamage(player.GetFullDamage(this.damage));
        }
        
    }
}
