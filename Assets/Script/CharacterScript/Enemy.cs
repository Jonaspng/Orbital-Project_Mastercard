using UnityEngine;
using System;


public abstract class Enemy : Unit {

    [SerializeField] private int enemyIndex;

    [SerializeField] private int moveNumber;

    [SerializeField] private int arrowStuckCount; 

    [SerializeField] private bool isImmobilised;

    [SerializeField] private bool isBurned;

    [SerializeField] private GameObject brokenIcon;

    public bool CheckImmobilised() {
        return this.isImmobilised;
    }

    public bool CheckBurn() {
        return this.isBurned;
    }

    public int GetMoveNumber() {
        return this.moveNumber;
    }

    public void SetMoveNumber(int i) {
        this.moveNumber = i;
    }

    public int GetEnemyIndex() {
        return this.enemyIndex;
    }

    public void SetEnemyIndex(int enemyIndex) {
        this.enemyIndex = enemyIndex;
    }

    public void receiveDamage(int damage, int index) {
        this.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        this.gameObject.GetComponent<Animator>().SetTrigger("Damaged");
        int realDamage;
    
        if (BrokenStatus()) {
            realDamage = (int) (int) Math.Round(damage * 1.25, MidpointRounding.AwayFromZero);
        } else {
            realDamage = damage;                
        }
        if (realDamage > GetBaseShield()) {
            print("real damage: " + realDamage);
            print("base shield: " + GetBaseShield());
            SetHealth(GetHealth() - realDamage + GetBaseShield());
        } 
        
        print(GetHealth());
        if (GetHealth() < 0) {
            this.gameObject.GetComponentInParent<BattleHUD>().SetHP(0);
        } else {
            this.gameObject.GetComponentInParent<BattleHUD>().SetHP(GetHealth());
        }
        if (realDamage >= GetBaseShield()) {
            print("shield destroyed");
            if (realDamage == GetBaseShield()) {
                DamageNumberAnimation("Blocked", Color.white);
            } else {
                DamageNumberAnimation(realDamage - GetBaseShield(), Color.red);
            }
            SetBaseShield(0);
            this.gameObject.GetComponentInParent<BattleHUD>().RemoveShieldIcon();
        } else {
            DamageNumberAnimation("Blocked", Color.white);
            SetBaseShield(GetBaseShield() - realDamage);
            this.gameObject.GetComponentInParent<BattleHUD>().RenderEnemyShieldIcon(enemyIndex);
        }
        if (GetHealth() <= 0) {
            this.gameObject.GetComponentInParent<BattleHUD>().RemoveIndicator();
            this.gameObject.GetComponent<Animator>().SetTrigger("Dead");
            StartCoroutine(StageManager.GetInstance().DestroyEnemy(enemyIndex));
            return;
        }
    }

    public void receiveOverTimeDamage(int damage, int index) {
        SetHealth(GetHealth() - damage);
        DamageNumberAnimation(damage, Color.red);
        if (GetHealth() <= 0) {
            this.gameObject.GetComponentInParent<BattleHUD>().SetHP(0);
            this.gameObject.GetComponent<Animator>().SetTrigger("Dead");
            StartCoroutine(StageManager.GetInstance().DestroyEnemy(enemyIndex));
        } else {
            this.gameObject.GetComponentInParent<BattleHUD>().SetHP(GetHealth());
        }
    }

    public int DamageTaken(int damage) {
        if (BrokenStatus()) {
            return (int) Math.Round((damage + arrowStuckCount * 2) * 1.25, MidpointRounding.AwayFromZero);
        }
        return damage + arrowStuckCount * 2;
    }

    public int FireballDamageTaken(int damage) {
        if (isBurned && BrokenStatus()) {
            return (int) Math.Round(damage * 1.25 * 1.25, MidpointRounding.AwayFromZero);
        } else if (BrokenStatus() || isBurned) {
            return (int) Math.Round(damage * 1.25, MidpointRounding.AwayFromZero);
        }
        return damage;
    }

    public void IncrementArrowStuckCount() {
        arrowStuckCount++;
    }

    public void ChangeIsImmobilised(bool status) {
        this.isImmobilised = status;
    }



    public void ChangeIsBurned(bool status) {
        this.isBurned = status;
    }
    
    // Method to calculate damage from arrow damage cards if StickyArrows has been used.
    public void ReceiveArrowDamage(Archer source, int damage, int enemyIndex) {
        if (source.CheckStickyArrow()) {
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
        return (int) Math.Round(GetAttackModifier() * (GetBaseAttack()), MidpointRounding.AwayFromZero);
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
