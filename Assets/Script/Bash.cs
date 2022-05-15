using UnityEngine;
using System.Linq;
using System.Collections;


public class Bash : Cards {

    public int damage;

    public Cards bashCard;

    public Bash(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }


    // Start is called before the first frame update
    void Start() {
        bashCard = new Bash(8, 2, 2);  
    }

    // Update is called once per frame
    void Update() {
        int enemyIndex = 0;
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            StageManager.instance.playerMove(bashCard, enemyIndex);
        }        
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        int currentTurn = StageManager.instance.currentTurn;
        Hashtable eventManager = StageManager.instance.eventManager;
        AbstractEvent[] newAttackModEvent = {new BrokenEvent(1, true, enemyIndex)};
        AbstractEvent[] newResetEvent = {new BrokenEvent(1, false, enemyIndex)};
        if (eventManager.Contains(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newAttackModEvent);
        } else {
            eventManager.Add(currentTurn + 1, newAttackModEvent);
        }
        if (eventManager.Contains(currentTurn + 2)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 2] = currEvent.Concat(newResetEvent);
        } else {
            eventManager.Add(currentTurn + 2, newResetEvent);
        }
        enemies[enemyIndex].receiveDamage(player.GetFullDamage(this.damage));
    }

}
