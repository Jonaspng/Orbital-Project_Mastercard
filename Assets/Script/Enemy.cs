using UnityEngine;
using System;

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

    public bool isBroken;

    public Enemy(int health, double attackModifier, double shieldModifier) {
        this.health = health;
        this.attackModifier = attackModifier;
        this.shieldModifier = shieldModifier;
    }

    public void receiveDamage(int damage) {
        if (health < 0) {
            //game over
        } else {
            if (isBroken) {
            health = (int) Math.Round(health - damage * 1.25);
            } else {
                health -= damage;
            }
        }
        
        
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

    public void ChangeIsBroken(bool status) {
        this.isBroken = status;
    }
    
    // Method to calculate damage from arrow damage cards if StickyArrows has been used.
    public void ReceiveArrowDamage(Archer source, int damage) {
        if (source.isStickyArrowEnabled) {
            receiveDamage(damage + arrowStuckCount * 2);
            arrowStuckCount++;
        } else {
            receiveDamage(damage);
        }
    }


    
    // eh shit we need change to abstract later lol
    //overtime damage will be done by stageManager

       

}
