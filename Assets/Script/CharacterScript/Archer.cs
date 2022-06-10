using UnityEngine;
using System;

[System.Serializable]
public class Archer : Player {

    public int evasionCount; 

    public bool isStickyArrowEnabled;

    public bool isStealthed;

    public bool evaded;

    public Archer(double attackModifier, 
    double shieldModifier) 
    : base(70, attackModifier, shieldModifier) { 
        
    }

    public void AddEvasionCount(int evasionCount) {
        this.evasionCount += evasionCount;
    }

    public override void receiveDamage(Enemy source, int damage, int enemyIndex) {
        // Effective damage calculation
        int realDamage;
        if (isBroken) {
            realDamage = (int) Math.Round(damage * 1.25, MidpointRounding.AwayFromZero);
        } else {
            realDamage = damage;
        }

        // special conditions
        if (realDamage >= this.baseShield) {
            if (isStealthed) {
                bool isAttacked = UnityEngine.Random.Range(0, 2) == 0;
                if (isAttacked) {
                    if (evasionCount > 0) {
                        DamageNumberAnimation("Evaded");
                        evaded = true;
                        evasionCount--;
                    } else {
                        evaded = false;
                        health = health - realDamage + this.baseShield;
                        DamageNumberAnimation(realDamage - this.baseShield);
                        ResetBaseShield();
                        StageManager.instance.playerHUD.RemoveShieldIcon();
                    }
                } else {
                    DamageNumberAnimation("Evaded");
                    evaded = true;
                }      
            } else {
                if (evasionCount > 0) {
                    DamageNumberAnimation("Evaded");
                    evaded = true;
                    evasionCount--;
                    GameObject.Find("PlayerHUD").GetComponent<BattleHUD>().RemoveDodgeIcon();
                } else {
                    evaded = false;
                    health = health - realDamage + this.baseShield;
                    DamageNumberAnimation(realDamage - this.baseShield);
                    ResetBaseShield();
                    StageManager.instance.playerHUD.RemoveShieldIcon();
                }           
            }
        } else {
            DamageNumberAnimation(0);
            StageManager.instance.playerHUD.RenderPlayerShieldIcon(-realDamage);
            this.baseShield -= realDamage;
        }
        StageManager.instance.playerHUD.SetHP(this.health);
    }

    public override void ChangeIsBroken(bool status) {
        if (!evaded) {
            this.isBroken = status;
            if (status) {
                GameObject.Find("PlayerHUD").GetComponent<BattleHUD>().RenderBrokenIcon();
            }            
        }
        evaded = false;
    }

    public void ChangeStickyArrowStatus(bool b) {
        isStickyArrowEnabled = b;
    }

    public override int GetFullDamage(int cardDamage) {
        return (int) Math.Round(this.attackModifier * (cardDamage + this.baseAttack));
    }  

    public void ChangeStealthStatus(bool status) {
        this.isStealthed = status;
    }
}
