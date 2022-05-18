using UnityEngine;

public class Dodge : Cards {

    public int evasionCount;

    public Dodge(int evasionCount, int turns, int manaCost) : base(manaCost, turns) {
        this.evasionCount = evasionCount;
    }

    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        player.addEvasionCount(this.evasionCount);
    }
}
