using UnityEngine;
using System;


public abstract class Enemy : Unit {

    public int enemyIndex;

    public int moveNumber;

    public int arrowStuckCount; 

    public bool isImmobilised;

    public bool isBurned;

    public bool isBroken;

    public GameObject brokenIcon;

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
            this.gameObject.GetComponentInParent<BattleHUD>().RemoveIndicator();
            this.gameObject.GetComponent<Animator>().SetTrigger("Dead");
            StartCoroutine(StageManager.instance.DestroyEnemy(enemyIndex));
            return;
        }
    }

    public void receiveOverTimeDamage(int damage, int index) {
        this.health = this.health - damage;
        DamageNumberAnimation(damage, Color.red);
        if (this.health <= 0) {
            this.gameObject.GetComponentInParent<BattleHUD>().SetHP(0);
            this.gameObject.GetComponent<Animator>().SetTrigger("Dead");
            StartCoroutine(StageManager.instance.DestroyEnemy(enemyIndex));
        } else {
            this.gameObject.GetComponentInParent<BattleHUD>().SetHP(this.health);
        }
    }

    public int DamageTaken(int damage) {
        if (isBroken) {
            return (int) Math.Round((damage + arrowStuckCount * 2) * 1.25, MidpointRounding.AwayFromZero);
        }
        return damage + arrowStuckCount * 2;
    }

    public int FireballDamageTaken(int damage) {
        if (isBurned && isBroken) {
            return (int) Math.Round(damage * 1.25 * 1.25, MidpointRounding.AwayFromZero);
        } else if (isBroken || isBurned) {
            return (int) Math.Round(damage * 1.25, MidpointRounding.AwayFromZero);
        }
        return damage;
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
    // Method to calculate damage from arrow damage cards if StickyArrows has been used.
    public int CalculateArrowDamage(int damage) {
        return damage + arrowStuckCount * 2;
    }

    // Method to calculate damage from "Fireball" card.
    public void ReceiveFireballDamage(int damage, int enemyIndex) {
        if (isBurned) {
            receiveDamage((int) Math.Round(damage * 1.25, MidpointRounding.AwayFromZero), enemyIndex);
        } else {
            receiveDamage(damage, enemyIndex);
        }
    }

    public int GetFullDamage() {
        return (int) Math.Round(this.attackModifier * (this.baseAttack), MidpointRounding.AwayFromZero);
    }    

    public void RenderBrokenIndicator() {
        GameObject broken = Instantiate(brokenIcon, this.gameObject.transform.GetChild(0));
        broken.GetComponent<Animator>().SetTrigger("Broken");
    }

    public virtual void EnemyMoveNumberGenerator() {
        moveNumber =  UnityEngine.Random.Range(1, 4);
    }

    public abstract void EnemyMove(Player player, Enemy[] enemies, int index);

    public abstract void RenderWarningIndicator();
       

}
