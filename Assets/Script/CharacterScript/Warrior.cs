using UnityEngine;
using System;


public class Warrior : Player {

    public bool isStrongWillpower;

    public bool isEndure;

    public int hitCount;

    public Warrior(double attackModifier, double shieldModifier) 
    : base(100, attackModifier, shieldModifier) { 
        //empty
    }

    public override void receiveDamage(Enemy Source, int damage, int enemyIndex) {
        int realDamage;
        if (isStrongWillpower) {
            hitCount++;
        }         
        if (isBroken) {
            realDamage = (int) Math.Round(damage * 1.25);
        } else {
            realDamage = damage;
        }
        if (realDamage > this.baseShield ) {
            if (this.isEndure && this.health - realDamage + this.baseShield <= 0) {
                this.health = 1;
                this.isEndure = false;
            
            } else if (health - realDamage + this.baseShield <= 0) {
                health = health - realDamage + this.baseShield;
                //probably some game over event
            } else {
                health = health - realDamage + this.baseShield;
            }
            ResetBaseShield();
        } else {
            this.baseShield -= realDamage;
        }
        print(health);
        if (realDamage >= this.baseShield) {
            StageManager.instance.playerHUD.RemoveShieldIcon();
        } else {
            StageManager.instance.playerHUD.RenderPlayerShieldIcon(this.baseShield - realDamage);
        }
        StageManager.instance.playerHUD.SetHP(this.health);
        
    }

    public void ChangeIsStrongWillpower(bool status) {
        this.isStrongWillpower = status;
    }

    public void ChangeIsEndure(bool status) {
        this.isEndure = status;
    }


    public override int GetFullDamage(int cardDamage) {
        if (this.isStrongWillpower) {
            return (int) Math.Round(this.attackModifier * (this.baseAttack + cardDamage + this.hitCount * 2));
        } else {
            return (int) Math.Round(this.attackModifier * cardDamage + baseAttack);
        }
    }

}
