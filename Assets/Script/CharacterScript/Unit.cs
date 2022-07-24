using UnityEngine;
using TMPro;

public abstract class Unit : MonoBehaviour {

    [SerializeField] private string unitName;

    [SerializeField] private int health;

    [SerializeField] private int maxHp;
    
    [SerializeField] private int baseAttack;

    [SerializeField] private double attackModifier;

    [SerializeField] private int baseShield;

    [SerializeField] private double shieldModifier;

    [SerializeField] private bool isPoisoned;

    [SerializeField] private Animator animator;

    [SerializeField] private AudioSource attackSource;

    [SerializeField] private AudioSource shieldSource;

    [SerializeField] private bool isBroken;

    public string GetUnitName() {
        return this.unitName;
    }

    public bool BrokenStatus() {
        return isBroken;
    }

    public void SetBrokenStatus(bool status) {
        this.isBroken = status;
    }

    public int GetHealth() {
        return this.health;
    }

    public int GetMaxHp() {
        return this.maxHp;
    }

    public int GetBaseAttack() {
        return this.baseAttack;
    }

    public double GetAttackModifier() {
        return this.attackModifier;
    }

    public int GetBaseShield() {
        return this.baseShield;
    }

    public double GetShieldModifier() {
        return this.shieldModifier;
    }

    public bool GetIsPoisoned() {
        return this.isPoisoned;
    }

    public AudioSource GetattackSource() {
        return this.attackSource;
    }

    public AudioSource GetshieldSource() {
        return this.shieldSource;
    }

    public void ChangeIsPoisoned(bool status) {
        this.isPoisoned = status;
    }

    public Animator GetAnimator() {
        return this.animator;
    }

    public void SetBaseAttack(int baseAttack) {
        this.baseAttack = baseAttack;
    }

    public void SetBaseShield(int baseShield) {
        this.baseShield = baseShield;
    }

    public void SetHealth(int health) {
        this.health = health;
    }

    public void SetAttackModifier(double attackModifier) {
        this.attackModifier = attackModifier;
     }
    
    public void SetShieldModifier(double shieldModifier) {
        this.shieldModifier = shieldModifier;
     }

    public void DamageNumberAnimation(int number, Color color) {
        print("Damage animation played");
        this.gameObject.transform.Find("Damage").GetComponent<TextMeshProUGUI>().color = color;
        this.gameObject.transform.Find("Damage").GetComponent<TextMeshProUGUI>().text = number.ToString();
        this.gameObject.transform.Find("Damage").GetComponent<Animator>().SetTrigger("Damaged");
    }

    public void DamageNumberAnimation(string message, Color color) {
        this.gameObject.transform.Find("Damage").GetComponent<TextMeshProUGUI>().color = color;
        this.gameObject.transform.Find("Damage").GetComponent<TextMeshProUGUI>().text = message;
        this.gameObject.transform.Find("Damage").GetComponent<Animator>().SetTrigger("Damaged");
    }

    public void PlayAttackSound() {
        attackSource.Play();
    }

    public void PlayShieldSound() {
        shieldSource.Play();
    }

       
}
