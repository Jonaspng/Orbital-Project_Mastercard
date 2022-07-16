using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Adrenaline : Cards {

    [SerializeField] private float damageModifier;

    [SerializeField] private Material material;

    [SerializeField] private bool dissolve;

    [SerializeField] private TextMeshProUGUI descriptionTag;

    private void Awake() {
        this.damageModifier = 1.25f;
        InitialiseValues("Do 25% more damage this turn.");
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
        player.SetAttackModifier(player.GetAttackModifier() * damageModifier);
        GameObject.Find("Player Battlestation").GetComponentInChildren<BattleHUD>().RenderAttackUpIcon();
    }
}
