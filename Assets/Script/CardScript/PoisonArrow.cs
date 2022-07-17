using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using TMPro;

class PoisonArrow : Cards {

    [SerializeField] private Material material;

    [SerializeField] private bool dissolve;

    [SerializeField] private TextMeshProUGUI descriptionTag;

    private void Awake() {
        InitialiseValues(8, 8, "Deal 8 damage. Apply poison for 2 turns.");
    }

    public override void RefreshString() {
        descriptionTag.text = "Deal " + GetDamage() + " damage. Apply poison for 2 turns. ";
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
        GameObject.Find("Current Hand").GetComponent<Testing>().ReArrangeCards();

    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        
        enemies[enemyIndex].receiveDamage(player.GetFullDamage(GetOriginalDamage()), enemyIndex);
        player.GetAnimator().SetTrigger("Attack");
        player.PlayAttackSound();

        if (enemies[enemyIndex] != null) {
            enemies[enemyIndex].ChangeIsPoisoned(true);
            enemies[enemyIndex].GetComponentInParent<BattleHUD>().RenderEnemyPoisonIcon();
        }

        int currentTurn = StageManager.GetInstance().GetCurrentTurn();
        Dictionary<int, AbstractEvent[]> eventManager = StageManager.GetInstance().GetEnemyEventManager();
        Dictionary<int, AbstractEvent[]> eventManager2 = StageManager.GetInstance().GetPlayerEventManager();
        AbstractEvent[] newEvent = {new OvertimeDamageEvent(3, enemyIndex)};
        AbstractEvent[] resetEvent = {new PoisonEvent(false, enemyIndex)};
        if (eventManager.ContainsKey(currentTurn)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn];
            eventManager[currentTurn] = currEvent.Concat(newEvent).ToArray();
        } else {
            eventManager.Add(currentTurn, newEvent);
        }
        if (eventManager.ContainsKey(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newEvent).ToArray();
        } else {
            eventManager.Add(currentTurn + 1, newEvent);
        }
         if (eventManager2.ContainsKey(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager2[currentTurn + 1];
            eventManager2[currentTurn + 1] = currEvent.Concat(resetEvent).ToArray();
        } else {
            eventManager2.Add(currentTurn + 1, resetEvent);
        }
        
    
    }

}