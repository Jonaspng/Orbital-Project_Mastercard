using UnityEngine;

public class Brace : Cards {

    public int shield;

    public Brace(int shield, int turns, int manaCost) : base(manaCost, turns) {
        this.shield = shield;
    }

    public override void OnDrop(int enemyIndex) {
        if (StageManager.instance.manaCount - this.manaCost >= 0) {
            StageManager.instance.playerMove(this, enemyIndex);
            GameObject.Destroy(this.transform.gameObject);
        }
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        player.changeBaseShield(this.shield);
    }
}
