using UnityEngine;

public class LegionRoar : Cards {

    public Cards legionRoarCard;


    public LegionRoar(int turns, 
    bool isAoe, int manaCost) : base(manaCost, turns, isAoe) {

    }

    // Start is called before the first frame update
    void Start() {
        legionRoarCard = new LegionRoar(1, true, 1);  
    }

    // Update is called once per frame
    void Update() {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(legionRoarCard);
        }        
    }

    public override void executeCard(Player player, Enemy enemy) {
       
    }


}
