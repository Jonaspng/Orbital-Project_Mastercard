using UnityEngine;

public class LegionRoar : Cards {

    public Cards legionRoarCard;


    public LegionRoar(int turns, int manaCost) : base(manaCost, turns) {

    }

    // Start is called before the first frame update
    void Start() {
        legionRoarCard = new LegionRoar(1, 1);  
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(legionRoarCard, enemyIndex);
        }        
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
       
    }


}
