using UnityEngine;
using System;

[System.Serializable]
public class Mage : Player {

    public bool isReflected;

    public Mage(double attackModifier, double shieldModifier) 
    : base(80, attackModifier, shieldModifier) { 
        //empty
    }

    public override void receiveDamage(Enemy source, int damage, int enemyIndex) {
        int realDamage;
        if (isBroken) {
            realDamage = (int) Math.Round(damage * 1.25, MidpointRounding.AwayFromZero);
        } else {
            realDamage = damage;
        }
    
        if (realDamage > this.baseShield) {
                health = health - realDamage + this.baseShield;
                ResetBaseShield();
        } else {
            this.baseShield -= realDamage;
        }
        if (isReflected) {
                int reflectedDamage = (int) Math.Round(0.75 * realDamage, MidpointRounding.AwayFromZero);
                source.receiveDamage(reflectedDamage, enemyIndex);
        }
        if (realDamage >= this.baseShield) {
            StageManager.instance.playerHUD.RemoveShieldIcon();
        } else {
            StageManager.instance.playerHUD.RenderPlayerShieldIcon(this.baseShield - realDamage);
        }
        StageManager.instance.playerHUD.SetHP(this.health);
    
    }

    public override int GetFullDamage(int cardDamage) {
        return (int) Math.Round(this.attackModifier * cardDamage);
    }

    public void ChangeReflectStatus(bool status) {
        this.isReflected = status;
    }


  
}
