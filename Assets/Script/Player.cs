using UnityEngine;

[System.Serializable]
public abstract class Player {

    public int health;
    
    public int baseAttack;

    public double attackModifier;

    public int baseShield;

    public double shieldModifier;

    //private Card[] deck;

    public Player(int health, int baseAttack, double attackModifier, 
    int baseShield, double shieldModifier) {
        this.health = health;
        this.baseAttack = baseAttack;
        this.baseShield = baseShield;
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
        this.baseShield += baseShield;
    }
    
    public void changeAttackModifier(double attackModifier) {
        this.attackModifier *= attackModifier;
    }

    public void changeShieldModifier(double shieldModifier) {
        this.shieldModifier *= shieldModifier;
    }

    public abstract void addEvasionCount( int evasionCount);

    

    
    //poison damage will be done by stageManager

       

}
