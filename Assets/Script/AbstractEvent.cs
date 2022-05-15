using UnityEngine;

abstract class AbstractEvent {

    /*
    1) OverTime damage event -- Depends on who is the target
    2) Add mana
    */
    public int numberOfTurns;

    public int enemyIndex;

    public AbstractEvent(int numberOfTurns, int enemyIndex) {
        this.numberOfTurns = numberOfTurns;
        this.enemyIndex = enemyIndex;
    }

    public abstract void executeEvent(Player player, Enemy[] enemies);

}