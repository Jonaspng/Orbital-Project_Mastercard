using UnityEngine;

public class MultipleShots : Cards {
    public int shotCount = Random.Range(2, 6);
    public int damage;

    public Cards multipleShotsCard;

    public MultipleShots(int shotCount, int damage, int turns, int manaCost) : base(manaCost, turns) {
        this.shotCount = shotCount;
        this.damage = damage;
    }

    
    // Start is called before the first frame update
    void Start() {
        multipleShotsCard = new MultipleShots(1, 3, 1, 1);  
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(multipleShotsCard, enemyIndex);
        }        
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        for (int i = 0; i < shotCount; i++) {
            enemies[enemyIndex].receiveDamage(this.damage);
        }      
    }
}
