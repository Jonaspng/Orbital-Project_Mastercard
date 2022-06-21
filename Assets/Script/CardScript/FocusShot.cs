using UnityEngine;
using UnityEngine.UI;

public class FocusShot : Cards {

    public Material material;

    public bool dissolve;

    public double damageModifier;

    public FocusShot(double damageModifier, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damageModifier = damageModifier;
    }

    private void Update() {
        if (this.dissolve) {
            material.SetFloat("_Fade", Mathf.MoveTowards(material.GetFloat("_Fade"), 0f, 2f * Time.deltaTime));
            Destroy(this.gameObject, 0.4f);
        }
    }
    
    public override void OnDrop(int enemyIndex) {
        material.SetFloat("_Fade",1f);
        this.GetComponentInChildren<Image>().material = material;
        this.dissolve = true;
        StageManager.instance.playerMove(this, enemyIndex);
        GameObject.Find("Current Hand").GetComponent<Testing>().ReArrangeCards();
        
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        player.AddAttackModifier(damageModifier);
        GameObject.Find("Player Battlestation").GetComponentInChildren<BattleHUD>().RenderAttackUpIcon();
    }
}
