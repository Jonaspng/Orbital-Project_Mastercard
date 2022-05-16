using UnityEngine;


[System.Serializable]
public abstract class Player : MonoBehaviour {

    public int health;
    
    public int baseAttack;

    public double attackModifier;

    public int baseShield;

    public double shieldModifier;

    public bool isBroken;

    //private Card[] deck;

    public Player(int health, double attackModifier, double shieldModifier) {
        this.health = health;
        this.attackModifier = attackModifier;
        this.shieldModifier = shieldModifier;
    }

    public abstract void receiveDamage(Enemy source, int damage);

    public abstract int getHealth();
    
    public abstract void changeBaseAttack(int baseAttack);

     public abstract void changeBaseShield(int baseShield);
    
    public abstract void changeAttackModifier(double attackModifier);

    public abstract void changeShieldModifier(double shieldModifier);

    public abstract void addEvasionCount(int evasionCount);

    public void ChangeIsBroken(bool status) {
        this.isBroken = status;
    }

    public abstract int GetFullDamage(int cardDamage);

   
    //poison damage will be done by stageManager

       
}
