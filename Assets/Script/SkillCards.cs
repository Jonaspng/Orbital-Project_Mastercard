using UnityEngine;

[System.Serializable]
public class SkillCards : Cards {

   public int damage;

   public float damageModifier;

   public int defense;

   public float defenseModifier;

   public int turns;

    public SkillCards(string name, int damage, 
    float damageModifier, int defense, float defenseModifier, int turns) : base(name) {
        this.damage = damage;
        this.damageModifier = damageModifier;
        this.defense = defense;
        this.defenseModifier = defenseModifier;
        this.turns = turns;
    }       

}
