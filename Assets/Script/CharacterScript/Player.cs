using UnityEngine;
using TMPro;

public abstract class Player : Unit {

    public bool isBroken;

    public GameObject brokenIcon;

    public Player(int health, double attackModifier, double shieldModifier) {
        this.health = health;
        this.attackModifier = attackModifier;
        this.shieldModifier = shieldModifier;
    }

    public abstract void receiveDamage(Enemy source, int damage, int enemyIndex);

    public void receivePoisonDamage(int damage) {
        health = health - damage;
        DamageNumberAnimation(damage, Color.red);
        StageManager.instance.playerHUD.SetHP(this.health);
    }

    public  int getHealth() {
        return health;
    }

    public void AddBaseAttack(int baseAttack) {
            this.baseAttack += baseAttack;        
    }

     public void AddBaseShield(int baseShield) {
        this.baseShield += baseShield;
    }
    
    public void AddAttackModifier(double attackModifier) {
        this.attackModifier *= attackModifier;
    }

    public void changeShieldModifier(double shieldModifier) {
        this.shieldModifier *= shieldModifier;
    }
    
    public virtual void ChangeIsBroken(bool status) {
        this.isBroken = status;
        if (status) {
            GameObject.Find("Player Battlestation").GetComponentInChildren<BattleHUD>().RenderBrokenIcon();
        }
    }

    public void ResetBaseShield() {
        this.baseShield = 0;
    }

    public void ResetAttackModifier() {
        this.attackModifier = 1;
        GameObject.Find("Player Battlestation").GetComponentInChildren<BattleHUD>().RemoveAttackUpIcon();
        GameObject.Find("Player Battlestation").GetComponentInChildren<BattleHUD>().RemoveMysticShieldIcon();
    }

    public abstract int GetFullDamage(int cardDamage);

   
    public void RenderBrokenIndicator() {
        GameObject broken = Instantiate(brokenIcon, this.gameObject.transform.GetChild(0));
        broken.GetComponent<Animator>().SetTrigger("Broken");
    }


       
}
