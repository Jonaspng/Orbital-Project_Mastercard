using UnityEngine;

[System.Serializable]
public abstract class Cards {

   public string name;

   public int manaCost;

    public Cards(string name, int manaCost) {
        this.name = name;
        this.manaCost = manaCost;
    }

    public void executeCard(Player player, Enemy enemy) {
        // do nothing
    }


}
