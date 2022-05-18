using UnityEngine;

public class LegionRoar : Cards {

    public LegionRoar(int turns, int manaCost) : base(manaCost, turns) {

    }

    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
       foreach(Enemy enemy in enemies) {
           enemy.changeAttackModifier(0.75);
       }
    }


}
