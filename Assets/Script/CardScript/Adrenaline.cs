using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Adrenaline : Cards {

    public double damageModifier;

    public Material material;

    public bool dissolve;

    public TextMeshProUGUI descriptionTag;

    public Adrenaline(double damageModifier, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damageModifier = damageModifier;
    }

    private void Awake() {
        this.damageModifier = 1.25f;
        this.description = "Do 25% more damage this turn.";
    }

    public override void RefreshString() {
        // does nothing
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
        StageManager.instance.playerMove(this, enemyIndex);;
        GameObject.Find("Current Hand").GetComponent<Testing>().ReArrangeCards();
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        player.AddAttackModifier(damageModifier);
        GameObject.Find("Player Battlestation").GetComponentInChildren<BattleHUD>().RenderAttackUpIcon();
    }
}
