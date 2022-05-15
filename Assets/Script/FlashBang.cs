using UnityEngine;
using System.Linq;
using System.Collections;

class FlashBang : Cards {

    public Cards flashBangCard;

    public FlashBang(int manaCost, int turns) 
    : base(manaCost, turns){

    }

    // Start is called before the first frame update
    void Start() {
        flashBangCard = new FlashBang(1, 1);  
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(flashBangCard, enemyIndex);
        }        
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyindex) {
        int currentTurn = StageManager.instance.currentTurn;
        Hashtable eventManager = StageManager.instance.eventManager;
        AbstractEvent[] newResetEvent = {new StunEvent(1, false, 0)};
        if (eventManager.Contains(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newResetEvent);
        } else {
            eventManager.Add(currentTurn + 1, newResetEvent);
        }
        enemies[enemyindex].ChangeIsImmobilised(true);
    }

}