using UnityEngine;

public class Adrenaline : Cards {

    public Cards adrenalineCard;

    public double damageModifier;

    public Adrenaline(double damageModifier, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damageModifier = damageModifier;
    }
    // Start is called before the first frame update
    void Start() {
        adrenalineCard = new Adrenaline(10, 1, 2);        
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(adrenalineCard, enemyIndex);
        }
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        player.changeAttackModifier(damageModifier);
    }
}
