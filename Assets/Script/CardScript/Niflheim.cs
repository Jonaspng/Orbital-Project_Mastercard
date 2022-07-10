using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Niflheim : Cards {

    public Material material;

    public bool dissolve;

    public TextMeshProUGUI descriptionTag;

    public Niflheim(int damage, int turns, int manaCost) : base(manaCost, turns) {
    }

    private void Awake() {
        this.originalDamage = 25;
        this.damage = 25;
        this.description = "Deal " + this.damage + " damage to all enemies.";
    }

    public override void RefreshString() {
        this.description = "Deal " + this.damage + " damage to all enemies.";
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
        for (int i = 0; i < enemies.Length; i ++) {
            if (enemies[i] != null) {
                enemies[i].receiveDamage(player.GetFullDamage(this.originalDamage), i);                
            }
        }
        
    }
}
