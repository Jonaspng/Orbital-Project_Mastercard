using UnityEngine;
using System.Linq;
using System.Collections;

public class AncientPower : Cards {

    public Cards ancientPowerCard;

    public int damage;

    public AncientPower(int damage, int turns, int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }
    // Start is called before the first frame update
    void Start() {
        ancientPowerCard = new AncientPower(5, 1, 2);        
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(ancientPowerCard, enemyIndex);
        }
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        int currentTurn = StageManager.instance.currentTurn;
        Hashtable eventManager = StageManager.instance.eventManager;
        AbstractEvent[] newAddEvent = {new DamageEvent(1, 5)};
        AbstractEvent[] newResetEvent = {new DamageEvent(1, -5)};
        if (eventManager.Contains(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newAddEvent);
        }
        if (eventManager.Contains(currentTurn + 2)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 2] = currEvent.Concat(newResetEvent);
        }
        eventManager.Add(currentTurn + 1, newAddEvent);
        eventManager.Add(currentTurn + 2, newResetEvent);
    }
}
