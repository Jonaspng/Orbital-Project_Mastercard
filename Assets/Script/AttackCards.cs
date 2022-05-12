using System;
using UnityEngine;

[System.Serializable]
public class AttackCards : Cards {

   public int damage;

   public double damageModifier;

   public int turns;

    public AttackCards(string name, int damage, double damageModifier, int turns) : base(name) {
        this.damage = damage;
        this.damageModifier = damageModifier;
        this.turns = turns;
    }

    public override void executeCard(Player player, Enemy enemy) {
        player.changeAttackModifier(damageModifier);
        enemy.receiveDamage((int) Math.Round(damage * damageModifier));
    }

}
