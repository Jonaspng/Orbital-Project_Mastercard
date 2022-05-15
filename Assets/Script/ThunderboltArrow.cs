using UnityEngine;

public class ThunderboltArrow : Cards {

    public int damage;

    public Cards thunderboltArrowCard;

    public ThunderboltArrow(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }


    // Start is called before the first frame update
    void Start() {
        thunderboltArrowCard = new ThunderboltArrow(8, 2, 2);  
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(thunderboltArrowCard, enemyIndex);
        }        
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        enemies[enemyIndex].receiveDamage(this.damage);
    }

}
