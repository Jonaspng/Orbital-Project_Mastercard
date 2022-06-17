using UnityEngine;
using System;
using TMPro;
using EZCameraShake;

[System.Serializable]
public abstract class Enemy : Unit {

    public int enemyIndex;

    public int arrowStuckCount; 

    public bool isImmobilised;

    public bool isBurned;

    public bool isBroken;

    public Enemy(int health, double attackModifier, double shieldModifier) {
        this.health = health;
        this.attackModifier = attackModifier;
        this.shieldModifier = shieldModifier;
    }

    public void receiveDamage(int damage, int index) {
        this.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        this.gameObject.GetComponent<Animator>().SetTrigger("Damaged");
        int realDamage;
    
        if (isBroken) {
            realDamage = (int) (int) Math.Round(damage * 1.25, MidpointRounding.AwayFromZero);
        } else {
            realDamage = damage;                
        }
        if (realDamage > this.baseShield) {
            print("real damage: " + realDamage);
            print("base shield: " + this.baseShield);
            health = health - realDamage + this.baseShield;
        } 
        
        print(health);
        if (this.health < 0) {
            this.gameObject.GetComponentInParent<BattleHUD>().SetHP(0);
        } else {
            this.gameObject.GetComponentInParent<BattleHUD>().SetHP(this.health);
        }
        if (realDamage >= this.baseShield) {
            print("shield destroyed");
            if (realDamage == this.baseShield) {
                DamageNumberAnimation("Blocked", Color.white);
            } else {
                DamageNumberAnimation(realDamage - this.baseShield, Color.red);
            }
            ResetBaseShield();
            this.gameObject.GetComponentInParent<BattleHUD>().RemoveShieldIcon();
        } else {
            DamageNumberAnimation("Blocked", Color.white);
            this.baseShield -= realDamage;
            this.gameObject.GetComponentInParent<BattleHUD>().RenderEnemyShieldIcon(enemyIndex);
        }
        if (health <= 0) {
            StartCoroutine(StageManager.instance.DestroyEnemy(enemyIndex));
            return;
        }
    }

    public void receiveOverTimeDamage(int damage, int index) {
        this.health = this.health - damage;
        DamageNumberAnimation(damage, Color.red);
        if (this.health < 0) {
            this.gameObject.GetComponentInParent<BattleHUD>().SetHP(0);
            StartCoroutine(StageManager.instance.DestroyEnemy(enemyIndex));
        } else {
            this.gameObject.GetComponentInParent<BattleHUD>().SetHP(this.health);
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
        this.gameObject.GetComponentInParent<BattleHUD>().RemoveAttackDownIcon();        
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
            receiveDamage((int) Math.Round(damage * 1.25, MidpointRounding.AwayFromZero), enemyIndex);
        } else {
            receiveDamage(damage, enemyIndex);
        }
    }

    public int GetFullDamage(int damage) {
        return (int) Math.Round(this.attackModifier * (damage + this.baseAttack), MidpointRounding.AwayFromZero);
    }
    

    public abstract void EnemyMove(Player player, Enemy[] enemies, int index);
    
    // eh shit we need change to abstract later lol
    //overtime damage will be done by stageManager

       

}
