using UnityEngine;

public class Adrenaline : Cards {

    public double damageModifier;

    public Adrenaline(double damageModifier, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damageModifier = damageModifier;
    }

    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        player.changeAttackModifier(damageModifier);
    }
}
