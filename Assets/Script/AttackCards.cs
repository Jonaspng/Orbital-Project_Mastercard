using System;
using UnityEngine;

[System.Serializable]
public class AttackCards : Cards {

   public int damage;

   public double damageModifier;

   public int turns;

   public bool isAOE;

    public AttackCards(string name, int damage, double damageModifier, int turns, bool isAOE, int manaCost) 
    : base(name, manaCost) {
        this.damage = damage;
        this.damageModifier = damageModifier;
        this.turns = turns;
        this.isAOE = isAOE;
    }

    public override void executeCard(Player player, Enemy enemy) {
        player.changeAttackModifier(damageModifier);
        enemy.receiveDamage((int) Math.Round(damage * damageModifier));
    }

    public override void executeCard(Player player) {
       //do nothing
    }

    public override void executeCard(Enemy enemy) {
        //do nothing
    }

}
