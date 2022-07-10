using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;


public class Bash : Cards {
    public Material material;

    public bool dissolve;

    public TextMeshProUGUI descriptionTag;

    public Bash(int damage, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.damage = damage;
    }
    private void Awake() {
        this.damage = 8;
        this.originalDamage = 8;
        this.description = "Deal " + this.damage +" damage. Enemy receives 25% more damage for 2 turns.";
    }

    public override void RefreshString() {
        descriptionTag.text = "Deal " + this.damage +" damage. Enemy receives 25% more damage for 2 turns.";
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
                
        AbstractEvent[] newResetEvent = {new BrokenEnemyEvent(1, false, enemyIndex)};
        
        if (eventManager.ContainsKey(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newResetEvent).ToArray();
        } else {
            eventManager.Add(currentTurn + 1, newResetEvent);
        }

        enemies[enemyIndex].receiveDamage(player.GetFullDamage(this.originalDamage), enemyIndex);

        player.animator.SetTrigger("Attack");

        if (enemies[enemyIndex] != null) {
            enemies[enemyIndex].ChangeIsBroken(true);
            enemies[enemyIndex].RenderBrokenIndicator();
            enemies[enemyIndex].GetComponentInParent<BattleHUD>().RenderBrokenIcon();
        }
        
    }
}
