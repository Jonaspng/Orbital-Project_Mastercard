using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fireball : Cards {

    public Material material;

    public bool dissolve;

    public TextMeshProUGUI descriptionTag;

    public Fireball(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }

    private void Awake() {
        this.originalDamage = 10;
        this.damage = 10;
        this.description = "Deal " + this.damage + " damage. If enemy is burned, deal 25% more.";
    }

    public override void RefreshString() {
        descriptionTag.text = "Deal " + this.damage + " damage. If enemy is burned, deal 25% more.";
    }

    private void Update() {
        if (this.dissolve) {
            material.SetFloat("_Fade", Mathf.MoveTowards(material.GetFloat("_Fade"), 0f, 2f * Time.deltaTime));
            Destroy(this.gameObject, 0.4f);
        }
    }
    
    public override void OnDrop(int enemyIndex) {
        foreach (Transform word in this.transform.Find("Frame").transform) {
            word.gameObject.SetActive(false);
        }
        material.SetFloat("_Fade",1f);
        this.GetComponentInChildren<Image>().material = material;
        this.dissolve = true;
        StageManager.instance.playerMove(this, enemyIndex);
        GameObject.Find("Current Hand").GetComponent<Testing>().ReArrangeCards();
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        player.animator.SetTrigger("Attack");
        player.PlayAttackSound();
        enemies[enemyIndex].ReceiveFireballDamage(player.GetFullDamage(this.originalDamage), enemyIndex);
    }
}
