using UnityEngine;


public class BasicAttack : Cards {

    public int damage;

    public BasicAttack(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }

    public override void OnDrop(int enemyIndex) {
        if (StageManager.instance.manaCount - this.manaCost >= 0) {
            StageManager.instance.playerMove(this, enemyIndex);
            GameObject.Destroy(this.transform.gameObject);
        } 
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        Mage mage = (Mage) player;
        mage.animator.SetTrigger("Attack");
        enemies[enemyIndex].receiveDamage(player.GetFullDamage(this.damage), enemyIndex);
        //mage.animator.ResetTrigger("Attack");
    }

}
