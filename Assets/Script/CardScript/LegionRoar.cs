using UnityEngine;

public class LegionRoar : Cards {

    public LegionRoar(int turns, int manaCost) : base(manaCost, turns) {

    }

    public override void OnDrop(int enemyIndex) {
        if (StageManager.instance.manaCount - this.manaCost >= 0) {
            StageManager.instance.playerMove(this, enemyIndex);
            GameObject.Destroy(this.transform.gameObject);
        }
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
       foreach(Enemy enemy in enemies) {
           if (enemy != null) {
               enemy.changeAttackModifier(0.75);
               enemy.GetComponentInParent<BattleHUD>().RenderAttackDownIcon();
           }
       }
    }


}
