using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;

public class SmokeBomb : Cards {

    public Material material;

    public bool dissolve;

    public SmokeBomb(int turns, 
    int manaCost) : base(manaCost, turns) {
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
        Archer archer = (Archer) player;
        int currentTurn = StageManager.instance.currentTurn;
        Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.playerEventManager;
        AbstractEvent[] newResetEvent = {new StealthEvent(1, false, enemyIndex)};
        if (eventManager.ContainsKey(currentTurn)) {
            AbstractEvent[] currEvent = (AbstractEvent[]) eventManager[currentTurn];
            eventManager[currentTurn] = currEvent.Concat(newResetEvent).ToArray();
        } else {
            eventManager.Add(currentTurn, newResetEvent);
        }
        archer.ChangeStealthStatus(true);
        GameObject.Find("Player Battlestation").GetComponentInChildren<BattleHUD>().RenderSmokeBombIcon();
    }
}
