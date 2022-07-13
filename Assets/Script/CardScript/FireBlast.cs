using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

class FireBlast : Cards {

    public Material material;

    public bool dissolve;

    public TextMeshProUGUI descriptionTag;

    public FireBlast(int manaCost, int damage, int turns) 
    : base(manaCost, turns){
        this.damage = damage;
    }

    private void Awake() {
        this.originalDamage = 8;
        this.damage = 8;
        this.description = "Deal " + this.damage +" damage. Apply burn for 2 turns.";
    }

    public override void RefreshString() {
        descriptionTag.text = "Deal " + this.damage +" damage. Apply burn for 2 turns.";
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
        int currentTurn = StageManager.instance.currentTurn;

        Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.enemyEventManager;
        Dictionary<int, AbstractEvent[]> eventManager2 = StageManager.instance.playerEventManager;
        AbstractEvent[] newEvent = {new OvertimeDamageEvent(3, 2, enemyIndex)};
        AbstractEvent[] resetEvent = {new BurnEvent(1, false, enemyIndex)};

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
        
        player.animator.SetTrigger("Attack");
        player.PlayAttackSound();
        enemies[enemyIndex].receiveDamage(player.GetFullDamage(this.originalDamage), enemyIndex);
        
        if (enemies[enemyIndex] != null) {
            enemies[enemyIndex].ChangeIsBurned(true);
            enemies[enemyIndex].GetComponentInParent<BattleHUD>().RenderEnemyBurnIcon();
        }

        
    
    }

}