using UnityEngine;

public class CardSelection : MonoBehaviour {

    public void OnMouseDown() {
        DeckID newDeck = StageManager.instance.deckManager.currentDeckID;
        newDeck.AddCardID(this.GetComponent<Cards>().id);
        StageManager.instance.deckManager.SaveJson(newDeck);
        GameObject.Find("GameManager").GetComponent<GameManager>().stageNumber += 1;
        foreach (Transform obj in GameObject.Find("NewCards").transform) {
            GameObject.Destroy(obj.gameObject);
        }
        GameObject.Find("StageManager").GetComponent<PopUpMenu>().PopUp();
        
        StageManager.instance.InitialiseBattle();
    }
}
