using UnityEngine;
using System;

[System.Serializable]
public class Archer : Player {

    public int evasionCount; 

    public bool isStickyArrowEnabled;

    public bool isStealthed;

    public Archer(double attackModifier, 
    double shieldModifier) 
    : base(70, attackModifier, shieldModifier) { 
        
    }

    public override void addEvasionCount(int evasionCount) {
        this.evasionCount += evasionCount;
    }

    public override void receiveDamage(int damage) {
        if (isStealthed) {
            bool isAttackDodged = UnityEngine.Random.Range(0, 2) == 1;
            if (isAttackDodged) {
                health -= 0;
            }
        } else {
            health -= damage;
        }
    }

    public override void receiveDamage(Enemy source, int damage)
    {
        throw new NotImplementedException();
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
        isStickyArrowEnabled = b;
    }

    public override int GetFullDamage(int cardDamage) {
        return (int) Math.Round(this.attackModifier * cardDamage);
    }  

    public void ChangeStealthStatus(bool status) {
        this.isStealthed = status;
    }
}
