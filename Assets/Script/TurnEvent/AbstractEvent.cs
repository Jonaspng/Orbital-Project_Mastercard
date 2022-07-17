using UnityEngine;

public abstract class AbstractEvent {

    [SerializeField] private int enemyIndex;

    public AbstractEvent(int enemyIndex) {
        this.enemyIndex = enemyIndex;
    }

    public int GetEnemyIndex() {
        return this.enemyIndex;
    }

    public abstract void executeEvent(Player player, Enemy[] enemies);

}