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

    // what defense card need to do in execute card method!
    // public override void executeCard(Player player) {
    //     player.changeBaseShield((int) Math.Round(shield * shieldModifier));
    // }


}
