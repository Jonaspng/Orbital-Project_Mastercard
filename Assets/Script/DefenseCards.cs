using System;
using UnityEngine;


[System.Serializable]
public class DefenseCards : Cards{

   public int shield;

   public double shieldModifier;

   public int turns;

    public DefenseCards(string name, int shield, float shieldModifier, int turns, int manaCost) 
    : base(name, manaCost) {
        this.shield = shield;
        this.shieldModifier = shieldModifier;
        this.turns = turns;
    }

    public override void executeCard(Player player) {
        player.changeBaseShield((int) Math.Round(shield * shieldModifier));
    }

    public override void executeCard(Enemy enemy) {
        //do nothing
    }

    public override void executeCard(Player player, Enemy enemy) {
        //do nothing
    }

}
