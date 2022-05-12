using UnityEngine;

[System.Serializable]
public abstract class Cards {

   public string name;

    public Cards(string name) {
        this.name = name;
    }

    public abstract void executeCard(Player player, Enemy enemy);

}
