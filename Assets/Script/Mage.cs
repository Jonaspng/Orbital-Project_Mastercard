using UnityEngine;

[System.Serializable]
public class Mage : Player {


    public Mage(double attackModifier, double shieldModifier) 
    : base(80, attackModifier, shieldModifier) { 
        //empty
    }

    public override void addEvasionCount(int evasionCount) {
        //do nothing
    }

    public override void receiveDamage(int damage) {
        health -= damage;
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


  
}
