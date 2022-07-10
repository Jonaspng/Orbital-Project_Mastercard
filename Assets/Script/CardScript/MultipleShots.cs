using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MultipleShots : Cards {
    public Material material;

    public bool dissolve;

    public TextMeshProUGUI descriptionTag;

    public MultipleShots(int shotCount, int damage, int turns, int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }

    private void Awake() {
        this.originalDamage = 3;
        this.damage = 3;
        this.description = "Shoot 2 - 5 arrows, each dealing " + this.damage  + " damage.";
    }

    public override void RefreshString() {
        descriptionTag.text = "Shoot 2 - 5 arrows, each dealing " + this.damage  + " damage.";
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
        Archer archer = (Archer) player;
        for (int i = 0; i < Random.Range(2, 6); i++) {
            if (enemies[enemyIndex] != null) {
                enemies[enemyIndex].ReceiveArrowDamage(archer, player.GetFullDamage(this.originalDamage), enemyIndex);
            }
        }      
    }
}
