using UnityEngine;

[System.Serializable]
public class AttackCards : Cards {

   public int damage;

   public float damageModifier;

   public int turns;

    public AttackCards(string name, int damage, float damageModifier, int turns) : base(name) {
        this.damage = damage;
        this.damageModifier = damageModifier;
        this.turns = turns;
    }       

}
