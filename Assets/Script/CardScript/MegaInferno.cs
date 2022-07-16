using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MegaInferno : Cards {

    [SerializeField] private Material material;

    [SerializeField] private bool dissolve;

    [SerializeField] private TextMeshProUGUI descriptionTag;

    private void Update() {
        if (this.dissolve) {
            material.SetFloat("_Fade", Mathf.MoveTowards(material.GetFloat("_Fade"), 0f, 2f * Time.deltaTime));
            Destroy(this.gameObject, 0.4f);
        }
    }

    private void Awake() {
        InitialiseValues(10, 10, "Deal 10 base damage to all enemies.");
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
        player.GetAnimator().SetTrigger("Attack");
        player.PlayAttackSound();
        for (int i = 0; i < enemies.Length; i ++) {
            if (enemies[i] != null) {
                enemies[i].receiveDamage(player.GetFullDamage(GetOriginalDamage()), i);
            }         
        }
    }
}
