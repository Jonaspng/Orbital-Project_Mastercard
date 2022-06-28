using UnityEngine;
using UnityEngine.UI;

public class SwordDance : Cards {

    public double attackModifier;

    public Material material;

    public bool dissolve;

    public SwordDance(double attackModifier, int turns, int manaCost) : base(manaCost, turns) {
        
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
        player.AddAttackModifier(2);
        GameObject.Find("Player Battlestation").GetComponentInChildren<BattleHUD>().RenderAttackUpIcon();
    }
}
