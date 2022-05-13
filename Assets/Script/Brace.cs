using UnityEngine;

public class Brace : Cards {

    public Cards braceCard;

    public int shield;

    public Brace(int shield, int turns, int manaCost) : base(manaCost, turns) {
        this.shield = shield;
    }
    // Start is called before the first frame update
    void Start() {
        braceCard = new Brace(10, 1, 2);        
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(braceCard, enemyIndex);
        }
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        player.changeBaseShield(this.shield);
    }
}
