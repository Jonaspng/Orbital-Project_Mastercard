using UnityEngine;

public class Dodge : Cards {

    public int evasionCount;

    public Cards dodgeCard;

    public Dodge(int evasionCount, int turns, int manaCost) : base(manaCost, turns) {
        this.evasionCount = evasionCount;
    }

    
    // Start is called before the first frame update
    void Start() {
        dodgeCard = new Dodge(1, 1, 1);  
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(dodgeCard, enemyIndex);
        }        
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        player.addEvasionCount(this.evasionCount);
    }
}
