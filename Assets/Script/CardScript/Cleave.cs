using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cleave : Cards {

    public Material material;

    public bool dissolve;

    public TextMeshProUGUI descriptionTag;

    public Cleave(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }
    private void Awake() {
        this.damage = 8;
        this.originalDamage = 8;
        this.description = "Deal 8 base damage to all enemies";
    }

    public override void RefreshString() {
        
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

    public override void executeCard(Player player, Enemy[] enemies, int enemyindex) {
        player.animator.SetTrigger("Attack");
        foreach (Enemy enemy in enemies) {
            if (enemy != null) {
                enemy.receiveDamage(player.GetFullDamage(this.originalDamage), enemyindex);
            }
        }
        
    }
}
