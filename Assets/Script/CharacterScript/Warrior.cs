using UnityEngine;
using System;
using EZCameraShake;

public class Warrior : Player {

    [SerializeField] private bool isStrongWillpower;

    [SerializeField] private bool isEndure;

    [SerializeField] private int hitCount;


    public override void receiveDamage(Enemy Source, int damage, int enemyIndex) {
        int realDamage;
        this.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
        this.GetAnimator().SetTrigger("Damaged");
        if (isStrongWillpower) {
            hitCount++;
        }
        if (BrokenStatus()) {
            realDamage = (int) Math.Round(damage * 1.25, MidpointRounding.AwayFromZero);
        } else {
            realDamage = damage;
        }
        if (realDamage >= GetBaseShield() ) {
            if (this.isEndure && GetHealth() - realDamage + GetBaseShield() <= 0) {
                SetHealth(1);
                this.isEndure = false;          
            } else {
                SetHealth(GetHealth() - realDamage + GetBaseShield());
            }
            if (realDamage == GetBaseShield()) {
                DamageNumberAnimation("Blocked", Color.white);
            } else {
                DamageNumberAnimation(realDamage - GetBaseShield(), Color.red);
            }            
            SetBaseShield(0);
            StageManager.GetInstance().GetPlayerHUD().RemoveShieldIcon();
        } else {
            DamageNumberAnimation("Blocked", Color.white);
            StageManager.GetInstance().GetPlayerHUD().RenderPlayerShieldIcon(-realDamage);
            SetBaseShield(GetBaseShield() - realDamage);
        }
        print(GetHealth());
        if (this.GetHealth() < 0) {
            StageManager.GetInstance().GetPlayerHUD().SetHP(0);
        } else {
            StageManager.GetInstance().GetPlayerHUD().SetHP(GetHealth());
        }
        
    }

    public void ChangeIsStrongWillpower(bool status) {
        this.isStrongWillpower = status;
    }

    public bool CheckEndure() {
        return this.isEndure;
    }

    public void ChangeIsEndure(bool status) {
        this.isEndure = status;
    }

    public void resetHitCount() {
        this.hitCount = 0;
    }

    public override int GetFullDamage(int cardDamage) {
        if (this.isStrongWillpower) {
            return (int) Math.Round(GetAttackModifier() * (GetBaseAttack() + cardDamage + this.hitCount * 2));
        } else {
            return (int) Math.Round(GetAttackModifier() * cardDamage + GetBaseAttack());
        }
    }

}
