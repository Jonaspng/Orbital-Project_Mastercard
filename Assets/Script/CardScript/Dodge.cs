using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dodge : Cards {

    public int evasionCount;

    public Material material;

    public bool dissolve;

    public TextMeshProUGUI descriptionTag;

    public Dodge(int evasionCount, int turns, int manaCost) : base(manaCost, turns) {
        this.evasionCount = evasionCount;
    }

    private void Awake() {
        this.description = "Evade 1 attack.";
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

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        Archer archer = (Archer) player;
        archer.AddEvasionCount(this.evasionCount);
        GameObject.Find("Player Battlestation").GetComponentInChildren<BattleHUD>().RenderDodgeIcon();
    }
}
