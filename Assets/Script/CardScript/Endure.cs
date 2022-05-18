using UnityEngine;

public class Endure : Cards {

    public Endure(int turns, int manaCost) : base(manaCost, turns) {

    }

    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        Warrior temp = (Warrior) player;
        temp.ChangeIsEndure(true);
    }


}
