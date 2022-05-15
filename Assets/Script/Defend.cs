using UnityEngine;

public class Defend : Cards {

    public int shield;

    public Cards defendCard;

    public Defend(int shield, int turns, int manaCost) : base(manaCost, turns) {
        this.shield = shield;
    }

    
    // Start is called before the first frame update
    void Start() {
        defendCard = new Defend(6, 1, 1);  
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(defendCard, enemyIndex);
        }        
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        player.changeBaseShield(this.shield);
    }
}
