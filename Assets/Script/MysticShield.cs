using UnityEngine;

public class MysticShield : Cards {

    public Cards mysticShieldCard;


    public MysticShield(int turns, int manaCost) : base(manaCost, turns) {

    }

    // Start is called before the first frame update
    void Start() {
        mysticShieldCard = new MysticShield(1, 1);  
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(mysticShieldCard, enemyIndex);
        }        
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        enemies[enemyIndex].changeAttackModifier(0.75);
    }


}
