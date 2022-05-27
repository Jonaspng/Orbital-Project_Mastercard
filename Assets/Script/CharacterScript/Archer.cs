using UnityEngine;
using System;

[System.Serializable]
public class Archer : Player {

    public int evasionCount; 

    public bool isStickyArrowEnabled;

    public bool isStealthed;

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
        if (realDamage > this.baseShield) {
            if (isStealthed) {
                bool isAttacked = UnityEngine.Random.Range(0, 2) == 0;
                if (isAttacked) {
                    if (evasionCount > 0) {
                        evasionCount--;
                    } else {
                        health = health - realDamage + this.baseShield;
                        ResetBaseShield();
                    }
                }                
            } else {
                if (evasionCount > 0) {
                    evasionCount--;
                } else {
                    health = health - realDamage + this.baseShield;
                    ResetBaseShield();
                }           
            }
        } else {
            this.baseShield -= realDamage;
        }
        if (realDamage >= this.baseShield) {
            StageManager.instance.playerHUD.RemoveShieldIcon();
        } else {
            StageManager.instance.playerHUD.RenderPlayerShieldIcon(this.baseShield - realDamage);
        }
        StageManager.instance.playerHUD.SetHP(this.health);
    }

    public override void ChangeIsBroken(bool status) {
        if (evasionCount == 0 && !isStealthed) {
            this.isBroken = status;
        }
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
