using UnityEngine;

public class StickyArrows : Cards {

    public Cards stickyArrowsCard;


    public StickyArrows(int turns, int manaCost) : base(manaCost, turns) {

    }

    // Start is called before the first frame update
    void Start() {
        stickyArrowsCard = new StickyArrows(1, 1);  
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(stickyArrowsCard, enemyIndex);
        }        
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        Archer archer = (Archer) player;
        archer.ChangeStickyArrowStatus(true);
    }


}
