using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Brace : Cards {

    public int shield;

    public Material material;

    public bool dissolve;

    public TextMeshProUGUI descriptionTag;

    public Brace(int shield, int damage, int turns, int manaCost) : base(manaCost, turns) {
        this.shield = shield;
        this.damage = damage;
    }

    private void Awake() {
        this.shield = 10;
        this.originalDamage = 10;
        this.damage = 10;
        this.description = "Gain 10 shield. Deal " + this.damage + " damage.";
    }

    public override void RefreshString() {
        descriptionTag.text = "Deal " + this.damage + " damage.";
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
        StageManager.instance.playerHUD.RenderPlayerShieldIcon(this.shield);
        GameObject.Find("Current Hand").GetComponent<Testing>().ReArrangeCards();
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        player.AddBaseShield(this.shield);
        enemies[enemyIndex].receiveDamage(player.GetFullDamage(this.originalDamage), enemyIndex);
    }
}
