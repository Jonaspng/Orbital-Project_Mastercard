using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class FocusMana : Cards {

    public int manaGained = 2;

    public Material outline;

    public bool dissolve;

    public FocusMana(int manaGained, int turns, 
    int manaCost) : base(manaCost, turns) {
        this.manaGained = manaGained;
    }

    private void Update() {
        if (this.dissolve) {
            outline.SetFloat("_noiseStrength", Mathf.MoveTowards(outline.GetFloat("_noiseStrength"), 1.25f, 0.5f * Time.deltaTime));
            Destroy(this.gameObject, 3f);
        } else {

        }
    }


    public override void OnDrop(int enemyIndex) {
        // if (StageManager.instance.manaCount - this.manaCost >= 0) {
            outline.SetFloat("_noiseStrength",0f);
            Transform frame = this.transform.Find("Frame");
            foreach(Transform obj in frame) {
                obj.GetComponent<TextMeshProUGUI>().text = "";
            }
            this.GetComponentInChildren<Image>().material = outline;
            this.dissolve = true;
            StageManager.instance.playerMove(this, enemyIndex);
            // GameObject.Destroy(this.transform.gameObject);
            GameObject.Find("Current Hand").GetComponent<Testing>().ReArrangeCards();
        //}
    }

    public override void executeCard(Player player, Enemy[] enemies, int enemyIndex) {
        int currentTurn = StageManager.instance.currentTurn;
        Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.playerEventManager;
        AbstractEvent[] newManaEvent = {new GainManaEvent(1, manaGained, enemyIndex)};
        if (eventManager.ContainsKey(currentTurn)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn];
            eventManager[currentTurn] = currEvent.Concat(newManaEvent).ToArray();
        } else {
            eventManager.Add(currentTurn, newManaEvent);
        }
    }
    
}
