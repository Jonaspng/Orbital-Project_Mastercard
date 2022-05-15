using UnityEngine;
using System.Linq;
using System.Collections;

public class FocusMana : Cards {

    public Cards focusManaCard;

    public int manaGained;

    public FocusMana(int manaGained, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.manaGained = manaGained;
    }
    // Start is called before the first frame update
    void Start() {
        focusManaCard = new FocusMana(2, 1, 1);        
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = -1;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(focusManaCard, enemyIndex);
        }
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        int currentTurn = StageManager.instance.currentTurn;
        Hashtable eventManager = StageManager.instance.eventManager;
        AbstractEvent[] newManaEvent = {new GainManaEvent(1, manaGained, enemyIndex)};
        AbstractEvent[] newResetEvent = {new GainManaEvent(1, 0 - manaGained, enemyIndex)};
        if (eventManager.Contains(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newManaEvent);
        } else {
            eventManager.Add(currentTurn + 1, newManaEvent);
        }
        if (eventManager.Contains(currentTurn + 2)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 2];
            eventManager[currentTurn + 2] = currEvent.Concat(newResetEvent);
        } else {
            eventManager.Add(currentTurn + 2, newResetEvent);
        }
    }
}
