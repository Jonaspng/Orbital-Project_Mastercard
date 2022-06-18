using UnityEngine;


public class ArcherBasicAttack : Cards {

    public int damage;

    public ArcherBasicAttack(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }

    public override void OnDrop(int enemyIndex) {
        if (StageManager.instance.manaCount - this.manaCost >= 0) {
            StageManager.instance.playerMove(this, enemyIndex);
            GameObject.Destroy(this.transform.gameObject);
            GameObject.Find("Current Hand").GetComponent<Testing>().ReArrangeCards();
        } 
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        Archer archer = (Archer) player;
        player.animator.SetTrigger("Attack");
        enemies[enemyIndex].ReceiveArrowDamage(archer, player.GetFullDamage(this.damage), enemyIndex);
    }

}
