using System;
using UnityEngine;

[System.Serializable]
public class SkillCards : Cards {

   public int damage;

   public double damageModifier;

   public int shield;

   public double shieldModifier;

   public int turns;

    public SkillCards(string name, int damage, 
    float damageModifier, int shield, float shieldModifier, int turns) : base(name) {
        this.damage = damage;
        this.damageModifier = damageModifier;
        this.shield = shield;
        this.shieldModifier = shieldModifier;
        this.turns = turns;
    }

    public override void executeCard(Player player, Enemy enemy) {
        player.changeAttackModifier(damageModifier);
        player.changeBaseShield((int) Math.Round(shield * shieldModifier));
        player.changeShieldModifier(shieldModifier);
    }  

}
