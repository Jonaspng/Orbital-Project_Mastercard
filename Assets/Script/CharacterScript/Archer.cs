using UnityEngine;
using System;
using EZCameraShake;

[System.Serializable]
public class Archer : Player {

    [SerializeField] private int evasionCount; 

    [SerializeField] private bool isStickyArrowEnabled;

    [SerializeField] private bool isStealthed;

    [SerializeField] private bool evaded;

    public void AddEvasionCount(int evasionCount) {
        this.evasionCount += evasionCount;
    }

    public bool CheckStickyArrow() {
        return this.isStickyArrowEnabled;
    }

    public void SetEvasionCount(int i) {
        this.evasionCount = i;
    }

    public override void receiveDamage(Enemy source, int damage, int enemyIndex) {
        // Effective damage calculation
        int realDamage;
        this.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
        if (BrokenStatus()) {
            realDamage = (int) Math.Round(damage * 1.25, MidpointRounding.AwayFromZero);
        } else {
            realDamage = damage;
        }

        // special conditions
        if (realDamage >= GetBaseShield()) {
            if (isStealthed) {
                bool isAttacked = UnityEngine.Random.Range(0, 2) == 0;
                if (isAttacked) {
                    if (evasionCount > 0) {
                        DamageNumberAnimation("Evaded", Color.white);
                        evaded = true;
                        evasionCount--;
                    } else {
                        evaded = false;
                        SetHealth(GetHealth() - realDamage + GetBaseShield());
                        if (realDamage == GetBaseShield()) {
                            DamageNumberAnimation("Blocked", Color.white);
                        } else {
                            DamageNumberAnimation(realDamage - GetBaseShield(), Color.red);
                        }   
                        this.GetAnimator().SetTrigger("Damaged");
                        SetBaseShield(0);
                        StageManager.GetInstance().GetPlayerHUD().RemoveShieldIcon();
                    }
                } else {
                    DamageNumberAnimation("Evaded", Color.white);
                    evaded = true;
                }      
            } else {
                if (evasionCount > 0) {
                    DamageNumberAnimation("Evaded", Color.white);
                    evaded = true;
                    evasionCount--;
                    StageManager.GetInstance().GetPlayerHUD().RemoveDodgeIcon();
                } else {
                    evaded = false;
                    SetHealth(GetHealth() - realDamage + GetBaseShield());
                    if (realDamage == GetBaseShield()) {
                        DamageNumberAnimation("Blocked", Color.white);
                    } else {
                        DamageNumberAnimation(realDamage - GetBaseShield(), Color.red);
                    }
                    this.GetAnimator().SetTrigger("Damaged");
                    SetBaseShield(0);
                    StageManager.GetInstance().GetPlayerHUD().RemoveShieldIcon();
                }           
            }
        } else {
            DamageNumberAnimation("Blocked", Color.white);
            StageManager.GetInstance().GetPlayerHUD().RenderPlayerShieldIcon(-realDamage);
            SetBaseShield(GetBaseAttack() - realDamage);
        }
        if (GetHealth() < 0) {
            StageManager.GetInstance().GetPlayerHUD().SetHP(0);
        } else {
            StageManager.GetInstance().GetPlayerHUD().SetHP(GetHealth());
        }
    }

    public override void ChangeIsBroken(bool status) {
        if (!evaded) {
            SetBrokenStatus(status);
            if (status) {
                GameObject.Find("Player Battlestation").GetComponentInChildren<BattleHUD>().RenderBrokenIcon();
            }            
        }
        evaded = false;
    }

    public void ChangeStickyArrowStatus(bool b) {
        isStickyArrowEnabled = b;
    }

    public override int GetFullDamage(int cardDamage) {
        return (int) Math.Round(GetAttackModifier() * (cardDamage + GetBaseAttack()));
    }  

    public void ChangeStealthStatus(bool status) {
        this.isStealthed = status;
    }
}
