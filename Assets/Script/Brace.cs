using UnityEngine;

public class Brace : Cards {

    public int shield;

    public Brace(int shield, int turns, int manaCost) : base(manaCost, turns) {
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
