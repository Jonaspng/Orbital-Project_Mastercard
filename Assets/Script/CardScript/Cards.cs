using UnityEngine;

[System.Serializable]
public abstract class Cards : MonoBehaviour {
    
    public int manaCost;

    public int turns;

    public Cards(int manaCost, int turns) {
        this.manaCost = manaCost;
        this.turns = turns;
    }

    public abstract void executeCard(Player player, Enemy[] enemies, int enemyindex);

}
