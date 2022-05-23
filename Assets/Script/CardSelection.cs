using UnityEngine;

public class CardSelection : MonoBehaviour {

    public void OnMouseDown() {
        DeckID newDeck = StageManager.instance.deckManager.currentDeckID;
        newDeck.AddCardID(this.GetComponent<Cards>().id);
        StageManager.instance.deckManager.SaveJson(newDeck);
    }
}
