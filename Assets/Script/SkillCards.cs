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
    float damageModifier, int shield, double shieldModifier, int turns, int manaCost) 
    : base(name, manaCost) {
        this.damage = damage;
        this.damageModifier = damageModifier;
        this.shield = shield;
        this.shieldModifier = shieldModifier;
        this.turns = turns;
    }

    public override void executeCard(Player player) {
        player.changeAttackModifier(damageModifier);
        player.changeBaseShield((int) Math.Round(shield * shieldModifier));
        player.changeShieldModifier(shieldModifier);
    }

    public override void executeCard(Enemy enemy) {
        enemy.changeAttackModifier(damageModifier);
        enemy.changeBaseShield((int) Math.Round(shield * shieldModifier));
        enemy.changeShieldModifier(shieldModifier);
    }

    public override void executeCard(Player player, Enemy enemy) {
        //do nothing
    }

}
