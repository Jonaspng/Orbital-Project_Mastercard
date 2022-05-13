using UnityEngine;

[System.Serializable]
public abstract class Cards : MonoBehaviour{

   public int manaCost;

   public int turns;

   public bool isAoe;

    public Cards(int manaCost, int turns, bool isAoe) {
        this.manaCost = manaCost;
        this.turns = turns;
        this.isAoe = isAoe;
    }

    public abstract void executeCard(Player player, Enemy enemy);

}
