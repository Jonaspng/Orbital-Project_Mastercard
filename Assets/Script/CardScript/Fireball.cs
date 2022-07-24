using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Fireball : Cards {

    [SerializeField] private Material material;

    [SerializeField] private bool dissolve;

    [SerializeField] private TextMeshProUGUI descriptionTag;

    private void Awake() {
        InitialiseValues(10, 10, "Deal 10 damage. If enemy is burned, deal 25% more.");
    }

    public override void RefreshString() {
        descriptionTag.text = "Deal " + GetDamage() + " damage. If enemy is burned, deal 25% more.";
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
        StageManager.GetInstance().playerMove(this, enemyIndex);
        GameObject.Find("Current Hand").GetComponent<FanShapeArranger>().ReArrangeCards();
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        player.GetAnimator().SetTrigger("Attack");
        player.PlayAttackSound();
        enemies[enemyIndex].ReceiveFireballDamage(player.GetFullDamage(GetOriginalDamage()), enemyIndex);
    }
}
