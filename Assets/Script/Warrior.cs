using UnityEngine;
using System;

[System.Serializable]
public class Warrior : Player {

    public bool isStrongWillpower = false;

    public bool isEndure = false;

    public int hitCount = 0;

    public Warrior(double attackModifier, double shieldModifier) 
    : base(100, attackModifier, shieldModifier) { 
        //empty
    }

    public override void addEvasionCount( int evasionCount) {
        //do nothing
    }

    public override void receiveDamage(int damage) {
        if (this.isEndure && this.health - damage < 0) {
            this.health = 1;
            this.isEndure = false;
        } else if (health - damage < 0) {
            //probably some game over event
        } else {
            this.health -= damage;
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

    public void ChangeIsStrongWillpower(bool status) {
        this.isStrongWillpower = status;
    }

    public void ChangeIsEndure(bool status) {
        this.isEndure = status;
    }


    public override int GetFullDamage(int cardDamage) {
        hitCount++;
        if (this.isStrongWillpower) {
            return (int) Math.Round(this.attackModifier * (this.baseAttack + this.hitCount * 2));
        } else {
            return (int) Math.Round(this.attackModifier * this.baseAttack);
        }
    }

}
