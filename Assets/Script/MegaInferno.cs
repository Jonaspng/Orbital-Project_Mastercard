using UnityEngine;

public class MegaInferno : Cards {

    public Cards megaInfernoCard;

    public int damage;

    public MegaInferno(int damage, int turns, int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }
    // Start is called before the first frame update
    void Start() {
        megaInfernoCard = new MegaInferno(10, 1, 2);        
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(megaInfernoCard, enemyIndex);
        }
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        foreach(Enemy enemy in enemies) {
            enemy.receiveDamage(this.damage);
        }
    }
}
