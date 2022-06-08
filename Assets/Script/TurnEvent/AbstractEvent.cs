using UnityEngine;

public abstract class AbstractEvent {

    public int numberOfTurns;

    public int enemyIndex;

    public AbstractEvent(int numberOfTurns, int enemyIndex) {
        this.numberOfTurns = numberOfTurns;
        this.enemyIndex = enemyIndex;
    }

    public abstract void executeEvent(Player player, Enemy[] enemies);

    public virtual void executeEvent(Player player) {
        // do nothing
    }

}