using UnityEngine;

public class SniperShot : Cards {

    public Cards sniperShotCard;

    public int damage;

    public SniperShot(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }
    // Start is called before the first frame update
    void Start() {
        sniperShotCard = new SniperShot(20, 1, 2);        
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(sniperShotCard, enemyIndex);
        }
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        foreach (Enemy enemy in enemies) {
            enemy.receiveDamage(this.damage);
        }
    }
}
