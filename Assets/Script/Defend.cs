using UnityEngine;

public class Defend : Cards {

    public int shield;

    public Defend(int shield, int turns, int manaCost) : base(manaCost, turns) {
        this.shield = shield;
    }
    
    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        player.changeBaseShield(this.shield);
    }
}
