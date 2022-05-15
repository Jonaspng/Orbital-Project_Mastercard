using UnityEngine;
using System.Linq;
using System.Collections;

public class Reflect : Cards {

    public Cards reflectCard;

    public Reflect(int turns, 
    int manaCost) : base(manaCost, turns) {

    }
    // Start is called before the first frame update
    void Start() {
        reflectCard = new Reflect(1, 2);        
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = -1;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(reflectCard, enemyIndex);
        }
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        Mage mage = (Mage) player;
        int currentTurn = StageManager.instance.currentTurn;
        Hashtable eventManager = StageManager.instance.eventManager;
        AbstractEvent[] newResetEvent = {new ReflectEvent(1, false, enemyIndex)};
        if (eventManager.Contains(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[]) eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newResetEvent);
        } else {
            eventManager.Add(currentTurn + 1, newResetEvent);
        }
        mage.ChangeReflectStatus(true);
    }
}
