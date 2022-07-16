using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhantasmalGreatSword : Cards {

    [SerializeField] private Material material;

    [SerializeField] private bool dissolve;

    [SerializeField] private TextMeshProUGUI descriptionTag;

    private void Awake() {
        InitialiseValues(30, 30, "Deal 30 damage.");
    }

    public override void RefreshString() {
        descriptionTag.text = "Deal " + GetDamage() +" damage.";
    }

    private void Update() {
        if (this.dissolve) {
            material.SetFloat("_Fade", Mathf.MoveTowards(material.GetFloat("_Fade"), 0f, 2f * Time.deltaTime));
            Destroy(this.gameObject, 0.4f);
        }
    }

    public override void OnDrop(int enemyIndex) {
        material.SetFloat("_Fade",1f);
        foreach (Transform word in this.transform.Find("Frame").transform) {
            word.gameObject.SetActive(false);
        }
        this.GetComponentInChildren<Image>().material = material;
        this.dissolve = true;
        StageManager.instance.playerMove(this, enemyIndex);
        GameObject.Find("Current Hand").GetComponent<Testing>().ReArrangeCards();
}

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        enemies[enemyIndex].receiveDamage(player.GetFullDamage(GetOriginalDamage()), enemyIndex);
        player.GetAnimator().SetTrigger("Attack");
        player.PlayAttackSound();
    }
}
