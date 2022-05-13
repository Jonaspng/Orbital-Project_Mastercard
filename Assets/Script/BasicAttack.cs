using UnityEngine;

public class BasicAttack : Cards {

    public int damage;

    public Cards attackCard;

    public BasicAttack(int damage, int turns, 
    bool isAoe, int manaCost) : base(manaCost, turns, isAoe) {
        this.damage = damage;
    }

    
    // Start is called before the first frame update
    void Start() {
        attackCard = new BasicAttack(6, 1, false, 1);  
    }

    // Update is called once per frame
    void Update() {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(attackCard);
        }        
    }

    public override void executeCard(Player player, Enemy enemy) {
        enemy.receiveDamage(this.damage);
    }
}
