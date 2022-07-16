using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using TMPro;

public class AncientPower : Cards {

    [SerializeField] private Material material;

    [SerializeField] private bool dissolve;

    [SerializeField] private TextMeshProUGUI descriptionTag;

    private void Awake() {
        InitialiseValues("Give 5 spell damage for next turn");
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

        Dictionary<int, AbstractEvent[]> eventManager = StageManager.instance.playerEventManager;
        AbstractEvent[] newAddEvent = {new PlayerDamageEvent(1, 5, enemyIndex)};
        AbstractEvent[] newResetEvent = {new PlayerDamageEvent(1, -5, enemyIndex)};

        
        if (eventManager.ContainsKey(currentTurn)) {
            AbstractEvent[] currEvent = (AbstractEvent[]) eventManager[currentTurn];
            eventManager[currentTurn] = currEvent.Concat(newAddEvent).ToArray();
        } else {
            eventManager.Add(currentTurn, newAddEvent);
        }
        if (eventManager.ContainsKey(currentTurn + 1)) {
            AbstractEvent[] currEvent = (AbstractEvent[])eventManager[currentTurn + 1];
            eventManager[currentTurn + 1] = currEvent.Concat(newResetEvent).ToArray();
        } else {
            eventManager.Add(currentTurn + 1, newResetEvent);
        }        
    }
    
}
