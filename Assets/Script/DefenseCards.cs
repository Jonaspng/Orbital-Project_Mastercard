using UnityEngine;

[System.Serializable]
public class DefenseCards : Cards{

   public int defense;

   public float defenseModifier;

   public int turns;

    public DefenseCards(string name, int defense, float defenseModifier, int turns) : base(name) {
        this.defense = defense;
        this.defenseModifier = defenseModifier;
        this.turns = turns;
    }

}
