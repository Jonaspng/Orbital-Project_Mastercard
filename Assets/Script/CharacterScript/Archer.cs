using UnityEngine;
using System;
using EZCameraShake;

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
        this.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
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
                        DamageNumberAnimation("Evaded", Color.white);
                        evaded = true;
                        evasionCount--;
                    } else {
                        evaded = false;
                        health = health - realDamage + this.baseShield;
                        if (realDamage == this.baseShield) {
                            DamageNumberAnimation("Blocked", Color.white);
                        } else {
                            DamageNumberAnimation(realDamage - this.baseShield, Color.red);
                        }   
                        this.animator.SetTrigger("Damaged");
                        ResetBaseShield();
                        StageManager.instance.playerHUD.RemoveShieldIcon();
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
                    GameObject.Find("PlayerHUD").GetComponent<BattleHUD>().RemoveDodgeIcon();
                } else {
                    evaded = false;
                    health = health - realDamage + this.baseShield;
                    if (realDamage == this.baseShield) {
                        DamageNumberAnimation("Blocked", Color.white);
                    } else {
                        DamageNumberAnimation(realDamage - this.baseShield, Color.red);
                    }
                    this.animator.SetTrigger("Damaged");
                    ResetBaseShield();
                    StageManager.instance.playerHUD.RemoveShieldIcon();
                }           
            }
        } else {
            DamageNumberAnimation("Blocked", Color.white);
            StageManager.instance.playerHUD.RenderPlayerShieldIcon(-realDamage);
            this.baseShield -= realDamage;
        }
        if (this.health < 0) {
            StageManager.instance.playerHUD.SetHP(0);
        } else {
            StageManager.instance.playerHUD.SetHP(this.health);
        }
    }

    public override void ChangeIsBroken(bool status) {
        if (!evaded) {
            this.isBroken = status;
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
        return (int) Math.Round(this.attackModifier * (cardDamage + this.baseAttack));
    }  

    public void ChangeStealthStatus(bool status) {
        this.isStealthed = status;
    }
}
