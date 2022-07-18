using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MultipleShots : Cards {
    [SerializeField] private Material material;

    [SerializeField] private bool dissolve;

    [SerializeField] private TextMeshProUGUI descriptionTag;

    private void Awake() {
        InitialiseValues(3, 3, "Shoot 2 - 5 arrows, each dealing 3 damage.");
    }

    public override void RefreshString() {
        descriptionTag.text = "Shoot 2 - 5 arrows, each dealing " + GetDamage() + " damage.";
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
        Archer archer = (Archer) player;
        for (int i = 0; i < Random.Range(2, 6); i++) {
            if (enemies[enemyIndex] != null) {
                enemies[enemyIndex].ReceiveArrowDamage(archer, player.GetFullDamage(GetOriginalDamage()), enemyIndex);
            }
        }      
    }
}
