using UnityEngine;
using System;

[System.Serializable]
public abstract class Enemy : Unit {

    public int arrowStuckCount; 

    public bool isImmobilised;

    public bool isBurned;

    public bool isBroken;

    public bool isPoisoned;

    public Enemy(int health, double attackModifier, double shieldModifier) {
        this.health = health;
        this.attackModifier = attackModifier;
        this.shieldModifier = shieldModifier;
    }

    public void receiveDamage(int damage) {
        int realDamage;
        if (health <= 0) {
            realDamage = 0;
            //game over
        } else {
            if (isBroken) {
                realDamage = (int) Math.Round(damage * 1.25);
            } else {
                realDamage = damage;                
            }
            if (damage > this.baseShield) {
                health = health - realDamage + this.baseShield;
                ResetBaseShield();
            } else {
                this.baseShield -= realDamage;
            }
            print(health);
            StageManager.instance.enemyHUD.SetHP(this.health);
        }
        if (realDamage >= this.baseShield) {
            print("shield destroyed");
            StageManager.instance.enemyHUD.RemoveShieldIcon();
        } else {
            StageManager.instance.enemyHUD.RenderEnemyShieldIcon(this.baseShield - realDamage, 0);
        }
        
        
    }

    public int getHealth() {
        return health;
    }

    public void changeBaseAttack(int baseAttack) {
        this.baseAttack = baseAttack;
    }

    public void AddBaseShield(int baseShield) {
        this.baseShield += baseShield;
    }

    public void ResetBaseShield() {
        this.baseShield = 0;
    }
    
    public void changeAttackModifier(double attackModifier) {
        this.attackModifier *= attackModifier;
    }

    public void ResetAttackModifier() {
        this.attackModifier = 1;
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

    public void ChangeIsBurned(bool status) {
        this.isBurned = status;
    }
    public void ChangeIsPoisoned(bool status) {
        this.isPoisoned = status;
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

    // Method to calculate damage from "Fireball" card.
    public void ReceiveFireballDamage(int damage) {
        if (isBurned) {
            receiveDamage((int) Math.Round(damage * 1.25));
        } else {
            receiveDamage(damage);
        }
    }

    public int GetFullDamage(int damage) {
        return (int) Math.Round(this.attackModifier * (damage + this.baseAttack));
    }
    

    public abstract void EnemyMove(Player player, Enemy[] enemies);
    
    // eh shit we need change to abstract later lol
    //overtime damage will be done by stageManager

       

}
