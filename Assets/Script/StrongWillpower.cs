using UnityEngine;

public class StrongWillpower : Cards {

    public Cards willpowerCard;

    public StrongWillpower(int turns, int manaCost) : base(manaCost, turns) {

    }

    // Start is called before the first frame update
    void Start() {
        willpowerCard = new StrongWillpower(1, 1);  
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(willpowerCard, enemyIndex);
        }        
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        Warrior temp = (Warrior) player;
        temp.ChangeIsStrongWillpower(true);
    }


}
