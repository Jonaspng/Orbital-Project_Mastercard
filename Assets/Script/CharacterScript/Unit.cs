using UnityEngine;
using TMPro;

public abstract class Unit : MonoBehaviour {

    public string unitName;

    public int health;

    public int maxHp;
    
    public int baseAttack;

    public double attackModifier;

    public int baseShield;

    public double shieldModifier;

    public bool isPoisoned;

    public Animator animator;

    public void ChangeIsPoisoned(bool status) {
        this.isPoisoned = status;
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

       
}
