using UnityEngine;
using System;

[System.Serializable]
public class Mage : Player {

    public bool isReflected;

    public Mage(double attackModifier, double shieldModifier) 
    : base(80, attackModifier, shieldModifier) { 
        //empty
    }

    public override void addEvasionCount(int evasionCount) {
        //do nothing
    }

    public override void receiveDamage(Enemy source, int damage) {
        if (isReflected) {
            int reflectedDamage = (int) Math.Round(0.75 * damage);
            health -= damage;
            source.receiveDamage(reflectedDamage);
        } else {
            health -= damage;
        }
    }

    public override int getHealth() {
        return health;
    }

    public override void changeBaseAttack(int baseAttack) {
        this.baseAttack = baseAttack;
    }

     public override void changeBaseShield(int baseShield) {
        this.baseShield += baseShield;
    }
    
    public override void changeAttackModifier(double attackModifier) {
        this.attackModifier *= attackModifier;
    }

    public override void changeShieldModifier(double shieldModifier) {
        this.shieldModifier *= shieldModifier;
    }

    public override int GetFullDamage(int cardDamage) {
        return (int) Math.Round(this.attackModifier * cardDamage);
    }

    public void ChangeReflectStatus(bool status) {
        this.isReflected = status;
    }


  
}
