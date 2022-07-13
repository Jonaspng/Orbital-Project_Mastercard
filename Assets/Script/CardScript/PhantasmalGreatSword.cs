using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhantasmalGreatSword : Cards {

    public Material material;

    public bool dissolve;

    public TextMeshProUGUI descriptionTag;

    public PhantasmalGreatSword(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }

    private void Awake() {
        this.originalDamage = 20;
        this.damage = 20;
        this.description = "Deal " + this.damage +" damage.";
    }

    public override void RefreshString() {
        descriptionTag.text = "Deal " + this.damage +" damage.";
    }

    private void Update() {
        if (this.dissolve) {
            material.SetFloat("_Fade", Mathf.MoveTowards(material.GetFloat("_Fade"), 0f, 2f * Time.deltaTime));
            Destroy(this.gameObject, 0.4f);
        }
    }

    public override void OnDrop(int enemyIndex) {
        material.SetFloat("_Fade",1f);
        foreach (Transform word in this.transform.Find("Frame").transform) {
            word.gameObject.SetActive(false);
        }
        this.GetComponentInChildren<Image>().material = material;
        this.dissolve = true;
        StageManager.instance.playerMove(this, enemyIndex);
        GameObject.Find("Current Hand").GetComponent<Testing>().ReArrangeCards();
}

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        enemies[enemyIndex].receiveDamage(player.GetFullDamage(this.originalDamage), enemyIndex);
        player.animator.SetTrigger("Attack");
        player.PlayAttackSound();
    }
}
