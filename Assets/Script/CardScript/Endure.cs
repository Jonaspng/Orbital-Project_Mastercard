using UnityEngine;

public class Endure : Cards {

    public Endure(int turns, int manaCost) : base(manaCost, turns) {

    }

    public override void OnDrop(int enemyIndex) {
        if (StageManager.instance.manaCount - this.manaCost >= 0) {
            StageManager.instance.playerMove(this, enemyIndex);
            GameObject.Destroy(this.transform.gameObject);
        }
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        Warrior temp = (Warrior) player;
        temp.ChangeIsEndure(true);
    }


}
