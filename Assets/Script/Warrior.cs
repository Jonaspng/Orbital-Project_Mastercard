using UnityEngine;

[System.Serializable]
public class Warrior : Player {

    bool isStrongWillpower = false;

    public Warrior(double attackModifier, double shieldModifier) 
    : base(100, attackModifier, shieldModifier) { 
        //empty
    }

    public override void addEvasionCount( int evasionCount) {
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

    public void changeIsStrongWillpower(bool status) {
        this.isStrongWillpower = status;
    }

}
