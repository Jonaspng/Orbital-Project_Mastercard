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
    

    // What attack cards need to do in execute card method
    // public override void executeCard(Player player, Enemy enemy) {
    //     player.changeAttackModifier(damageModifier);
    //     enemy.receiveDamage((int) Math.Round(damage * damageModifier));
    // }
}
