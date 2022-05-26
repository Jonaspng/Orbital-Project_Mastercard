using UnityEngine;
using System;

[System.Serializable]
public abstract class Enemy : Unit {

    public int enemyIndex;

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

    public void receiveDamage(int damage, int index) {
        int realDamage;
        if (health <= 0) {
            realDamage = 0;
            //game over
        } else {
            if (isBroken) {
                realDamage = (int) (int) Math.Round(damage * 1.25, MidpointRounding.AwayFromZero);
            } else {
                realDamage = damage;                
            }
            if (realDamage > this.baseShield) {
                print("real damage: " + realDamage);
                print("base shield: " + this.baseShield);
                health = health - realDamage + this.baseShield;
                ResetBaseShield();
            } else {
                this.baseShield -= realDamage;
            }
            if (health <= 0) {
                StageManager.instance.DestroyEnemy(enemyIndex);
                return;
            }
            print(health);
            this.gameObject.GetComponentInParent<BattleHUD>().SetHP(this.health);
        }
        if (realDamage >= this.baseShield) {
            print("shield destroyed");
            this.gameObject.GetComponentInParent<BattleHUD>().RemoveShieldIcon();
        } else {
            this.gameObject.GetComponentInParent<BattleHUD>().RenderEnemyShieldIcon(this.baseShield - realDamage, enemyIndex);
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
    public void ReceiveArrowDamage(Archer source, int damage, int enemyIndex) {
        if (source.isStickyArrowEnabled) {
            receiveDamage(damage + arrowStuckCount * 2, enemyIndex);
            arrowStuckCount++;
        } else {
            receiveDamage(damage, enemyIndex);
        }
    }

    // Method to calculate damage from "Fireball" card.
    public void ReceiveFireballDamage(int damage, int enemyIndex) {
        if (isBurned) {
            receiveDamage((int) Math.Round(damage * 1.25), enemyIndex);
        } else {
            receiveDamage(damage, enemyIndex);
        }
    }

    public int GetFullDamage(int damage) {
        return (int) Math.Round(this.attackModifier * (damage + this.baseAttack));
    }
    

    public abstract void EnemyMove(Player player, Enemy[] enemies, int index);
    
    // eh shit we need change to abstract later lol
    //overtime damage will be done by stageManager

       

}
