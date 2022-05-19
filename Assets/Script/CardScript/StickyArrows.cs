using UnityEngine;

public class StickyArrows : Cards {

    public StickyArrows(int turns, int manaCost) : base(manaCost, turns) {

    }
    
    public void OnMouseDown() {
        int enemyIndex = 0;
        if (StageManager.instance.manaCount - this.manaCost >= 0) {
            StageManager.instance.playerMove(this, enemyIndex);
            GameObject.Destroy(this.transform.gameObject);
        }
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        Archer archer = (Archer) player;
        archer.ChangeStickyArrowStatus(true);
    }


}
