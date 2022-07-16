using System;
using EZCameraShake;
using UnityEngine;

[System.Serializable]
public class Mage : Player {

    public bool isReflected;

    public override void receiveDamage(Enemy source, int damage, int enemyIndex) {
        int realDamage;
        this.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        this.GetAnimator().SetTrigger("Damaged");
        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
        if (isBroken) {
            realDamage = (int) Math.Round(damage * 1.25, MidpointRounding.AwayFromZero);
        } else {
            realDamage = damage;
        }
    
        if (realDamage >= GetBaseShield()) {
                SetHealth(GetHealth() - realDamage + GetBaseShield());
                if (realDamage == GetBaseShield()) {
                    DamageNumberAnimation("Blocked", Color.white);
                } else {
                    DamageNumberAnimation(realDamage - GetBaseShield(), Color.red);
                }   
                SetBaseShield(0);
                StageManager.instance.playerHUD.RemoveShieldIcon();
        } else {
            DamageNumberAnimation("Blocked", Color.white);
            StageManager.instance.playerHUD.RenderPlayerShieldIcon(-realDamage);
            SetBaseShield(GetBaseShield() - realDamage);
        }
        if (isReflected) {
            int reflectedDamage = (int) Math.Round(0.75 * realDamage, MidpointRounding.AwayFromZero);
            if (source != null) {
                source.receiveDamage(reflectedDamage, enemyIndex);
            }
        }

        if (GetHealth() < 0) {
            StageManager.instance.playerHUD.SetHP(0);
        } else {
            StageManager.instance.playerHUD.SetHP(GetHealth());
        }
    
    }

    public override int GetFullDamage(int cardDamage) {
        return (int) Math.Round(GetAttackModifier() * (cardDamage + GetBaseAttack()));
    }

    public void ChangeReflectStatus(bool status) {
        this.isReflected = status;
    }


  
}
