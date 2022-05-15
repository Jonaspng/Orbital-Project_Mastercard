using UnityEngine;

public class ScatterShot : Cards {

    public Cards scatterShotCard;

    public int damage;

    public ScatterShot(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }

    public 
    // Start is called before the first frame update
    void Start() {
        scatterShotCard = new ScatterShot(8, 1, 1);  
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(scatterShotCard, enemyIndex);
        }        
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyindex) {
        foreach (Enemy enemy in enemies) {
            enemy.receiveDamage(this.damage);
        }
        
    }
}
