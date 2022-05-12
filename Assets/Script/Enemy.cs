using UnityEngine;

[System.Serializable]
public class Enemy {

    public int health;
    
    public int baseAttack;

    public double attackModifier;

    public int baseShield;

    public double shieldModifier;

    public int arrowStuckCount;


    public Enemy(int health, int baseAttack, double attackModifier, int baseShield, double shieldModifier) {
        this.health = health;
        this.baseAttack = baseAttack;
        this.attackModifier = attackModifier;
        this.baseShield = baseShield;
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
        this.baseShield = baseShield;
    }
    
    public void changeAttackModifier(double attackModifier) {
        this.attackModifier = attackModifier;
    }

    public void changeShieldModifier(double shieldModifier) {
        this.shieldModifier = shieldModifier;
    }

    public void IncrementArrowStuckCount() {
        arrowStuckCount++;
    }

    

    
    //overtime damage will be done by stageManager

       

}
