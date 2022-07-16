using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Defend : Cards {

    [SerializeField] private int shield;

    [SerializeField] private Material outline;

    [SerializeField] private bool dissolve;

    [SerializeField] private TextMeshProUGUI descriptionTag;

    private void Awake() {
        InitialiseValues("Gain 6 shield.");
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
        player.SetBaseShield(player.GetBaseShield() + this.shield);
        player.PlayShieldSound();
    }
}
