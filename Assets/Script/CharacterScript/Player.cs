using UnityEngine;

public abstract class Player : Unit {


    public bool isBroken;

    //private Card[] deck;

    public Player(int health, double attackModifier, double shieldModifier) {
        this.health = health;
        this.attackModifier = attackModifier;
        this.shieldModifier = shieldModifier;
    }

    public abstract void receiveDamage(Enemy source, int damage, int enemyIndex);

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
            GameObject.Find("PlayerHUD").GetComponent<BattleHUD>().RenderBrokenIcon();
        }
    }

    public void ResetBaseShield() {
        this.baseShield = 0;
    }

    public void ResetAttackModifier() {
        this.attackModifier = 1;
        GameObject.Find("PlayerHUD").GetComponent<BattleHUD>().RemoveAttackUpIcon();
        GameObject.Find("PlayerHUD").GetComponent<BattleHUD>().RemoveMysticShieldIcon();
    }

    public abstract int GetFullDamage(int cardDamage);

   
    //poison damage will be done by stageManager

       
}
