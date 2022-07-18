using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class ArcaneBolt : Cards {

    [SerializeField] private Material material;

    [SerializeField] private bool dissolve;

    [SerializeField] private TextMeshProUGUI descriptionTag;

    private void Awake() {
        InitialiseValues(10, 10, "Deal 10 damage. Enemy receives 25% more damage for 2 turns.");
    }

    public override void RefreshString() {
        descriptionTag.text = "Deal " + GetDamage() +" damage. Enemy receives 25% more damage for 2 turns.";
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
        int currentTurn = StageManager.GetInstance().GetCurrentTurn();
        Dictionary<int, AbstractEvent[]> eventManager = StageManager.GetInstance().GetEnemyEventManager();
        AbstractEvent[] newResetEvent = {new BrokenEnemyEvent(false, enemyIndex)};
        if (eventManager.ContainsKey(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newResetEvent).ToArray();
        } else {
            eventManager.Add(currentTurn + 1, newResetEvent);
        }
        enemies[enemyIndex].receiveDamage(player.GetFullDamage(GetOriginalDamage()), enemyIndex);
        player.GetAnimator().SetTrigger("Attack");
        player.PlayAttackSound();
        if (enemies[enemyIndex] != null) {
            enemies[enemyIndex].SetBrokenStatus(true);
            enemies[enemyIndex].RenderBrokenIndicator();
            enemies[enemyIndex].GetComponentInParent<BattleHUD>().RenderBrokenIcon();
        }
    }

}
