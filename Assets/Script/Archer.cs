using UnityEngine;

[System.Serializable]
public class Archer : Player {

    public int evasionCount; 

    public bool isStickyArrow;

    public Archer( double attackModifier, 
    double shieldModifier) 
    : base(70, attackModifier, shieldModifier) { 
        
    }

    public override void addEvasionCount(int evasionCount) {
        this.evasionCount += evasionCount;
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

    public void ChangeStickyArrowStatus(bool b) {
        isStickyArrow = b;
    }


  
}
