using UnityEngine;

public class MysticShield : Cards {

    public MysticShield(int turns, int manaCost) : base(manaCost, turns) {

    }

    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        enemies[enemyIndex].changeAttackModifier(0.75);
    }


}
