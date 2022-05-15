using UnityEngine;

public class SwordDance : Cards {

    public Cards swordDanceCard;

    public double attackModifier;

    public SwordDance(double attackModifier, int turns, int manaCost) : base(manaCost, turns) {
        this.attackModifier = attackModifier;
    }
    // Start is called before the first frame update
    void Start() {
        swordDanceCard = new SwordDance(2.0, 1, 1);        
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(swordDanceCard, enemyIndex);
        }
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        // subject to changes
        player.changeAttackModifier(this.attackModifier);
    }
}
