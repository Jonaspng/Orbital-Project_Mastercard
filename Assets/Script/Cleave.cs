using UnityEngine;

public class Cleave : Cards {

    public Cards cleaveCard;

    public int damage;

    public Cleave(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }

    public 
    // Start is called before the first frame update
    void Start() {
        cleaveCard = new Cleave(8, 1, 1);  
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(cleaveCard, enemyIndex);
        }        
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyindex) {
        foreach (Enemy enemy in enemies) {
            enemy.receiveDamage(this.damage);
        }
        
    }
}
