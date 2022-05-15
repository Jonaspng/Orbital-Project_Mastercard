using UnityEngine;
using System.Linq;
using System.Collections;

class PoisonArrow : Cards {

    public Cards poisonArrowCard;

    public int damage;

    public PoisonArrow(int damage, int manaCost, int turns) 
    : base(manaCost, turns){
        this.damage = damage;
    }

    void Start() {
        poisonArrowCard = new PoisonArrow(6, 1, 1);  
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(poisonArrowCard, enemyIndex);
        }        
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyindex) {
        enemies[enemyindex].receiveDamage(player.GetFullDamage(6));

        // The part that applies poison. Implementation to be improved. 
         int currentTurn = StageManager.instance.currentTurn;
        Hashtable eventManager = StageManager.instance.eventManager;
        AbstractEvent[] newEvent = {new OvertimeDamageEvent(3, 2, enemyindex)};
        AbstractEvent[] resetEvent = {new PoisonEvent(1, false, enemyindex)};
        if (eventManager.Contains(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newEvent);
        } else {
            eventManager.Add(currentTurn + 1, newEvent);
        }
        if (eventManager.Contains(currentTurn + 2)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 2];
            eventManager[currentTurn + 2] = currEvent.Concat(newEvent);
        } else {
            eventManager.Add(currentTurn + 2, newEvent);
        }
         if (eventManager.Contains(currentTurn + 3)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 3];
            eventManager[currentTurn + 2] = currEvent.Concat(resetEvent);
        } else {
            eventManager.Add(currentTurn + 2, resetEvent);
        }
        enemies[enemyindex].ChangeIsPoisoned(true);
    
    }

}