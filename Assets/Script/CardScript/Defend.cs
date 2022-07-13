using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Defend : Cards {

    public int shield;

    public Material outline;

    public bool dissolve;

    public TextMeshProUGUI descriptionTag;

    public Defend(int shield, int turns, int manaCost) : base(manaCost, turns) {
        this.shield = shield;
    }

    private void Awake() {
        this.description = "Gain 6 shield.";
    }

    public override void RefreshString() {
        
    }


    private void Update() {
        if (this.dissolve) {
            outline.SetFloat("_Fade", Mathf.MoveTowards(outline.GetFloat("_Fade"), 0f, 2f * Time.deltaTime));
            Destroy(this.gameObject, 0.4f);
        }
    }


    public override void OnDrop(int enemyIndex) {
        foreach (Transform word in this.transform.Find("Frame").transform) {
            word.gameObject.SetActive(false);
        }
        outline.SetFloat("_Fade",1f);
        this.GetComponentInChildren<Image>().material = outline;
        this.dissolve = true;
        StageManager.instance.playerHUD.RenderPlayerShieldIcon(this.shield);
        StageManager.instance.playerMove(this, enemyIndex);
        GameObject.Find("Current Hand").GetComponent<Testing>().ReArrangeCards();
   
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        player.AddBaseShield(this.shield);
        player.PlayShieldSound();
    }
}
