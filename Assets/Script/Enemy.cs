using UnityEngine;

[System.Serializable]
public class Enemy {

    public int health;
    
    public int baseAttack;

    public double attackModifier;

    public int baseShield;

    public double shieldModifier;

    public int arrowStuckCount;

    public bool isImmobilised;

    public bool isBurned;

    public Enemy(int health, double attackModifier, double shieldModifier) {
        this.health = health;
        this.attackModifier = attackModifier;
        this.shieldModifier = shieldModifier;
    }

    public void receiveDamage(int damage) {
    
        health -= damage;
    }

    public int getHealth() {
        return health;
    }

    public void changeBaseAttack(int baseAttack) {
        this.baseAttack = baseAttack;
    }

     public void changeBaseShield(int baseShield) {
        this.baseShield += baseShield;
    }
    
    public void changeAttackModifier(double attackModifier) {
        this.attackModifier *= attackModifier;
    }

    public void changeShieldModifier(double shieldModifier) {
        this.shieldModifier *= shieldModifier;
    }

    public void IncrementArrowStuckCount() {
        arrowStuckCount++;
    }

    public void ChangeIsImmobilised(bool status) {
        this.isImmobilised = status;
    }



    
    //overtime damage will be done by stageManager

       

}
