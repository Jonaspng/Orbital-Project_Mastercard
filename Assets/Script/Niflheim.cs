using UnityEngine;

public class Niflheim : Cards {

    public int damage;

    public Cards niflheimCard;

    public Niflheim(int damage, int turns, int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }

    
    // Start is called before the first frame update
    void Start() {
        niflheimCard = new Niflheim(15, 1, 3);  
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(niflheimCard, enemyIndex);
        }        
    }

     public override void executeCard(Player player, Enemy[] enemies, int enemyindex) {
        foreach (Enemy enemy in enemies) {
            enemy.receiveDamage(this.damage);
        }
        
    }
}
