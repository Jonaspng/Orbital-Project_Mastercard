using UnityEngine;

public class StickyArrows : Cards {

    public StickyArrows(int turns, int manaCost) : base(manaCost, turns) {

    }
    
    public void OnMouseDown() {
        int enemyIndex = 0;
        StageManager.instance.playerMove(this, enemyIndex);
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        Archer archer = (Archer) player;
        archer.ChangeStickyArrowStatus(true);
    }


}
