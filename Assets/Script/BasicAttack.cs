using UnityEngine;

public class BasicAttack : Cards {

    public int damage;

    public Cards attackCard;

    public BasicAttack(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }

    
    // Start is called before the first frame update
    void Start() {
        attackCard = new BasicAttack(6, 1, 1);  
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(attackCard, enemyIndex);
        }        
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        enemies[enemyIndex].receiveDamage(this.damage + player.baseAttack);
    }
}
