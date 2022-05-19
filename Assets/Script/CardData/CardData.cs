using UnityEngine;

[System.Serializable]
public class CardData {

   public int manaCost;

   public int turns;

   public CardData(int manaCost, int turns) {
        this.manaCost = manaCost;
        this.turns = turns;
    }

   public virtual void executeCard(Player player, Enemy[] enemies, int enemyindex) {

   }
}
