using System.IO;
using UnityEngine;
using System.Collections.Generic;

[DefaultExecutionOrder(-1)]
[System.Serializable]
public class DeckManager : MonoBehaviour {

    public string deckPath;

    public DeckID currentDeckID;
    
    public Deck currentDeck;

    public Deck usedPile;
    
    public Deck unusedPile;

    public Deck currentHand;
    
    public List<GameObject> prefabList;

    public GameObject currentHandPanel;

    public GameObject cardselectionPanel;


    public void DeckIdToDeck() {
        currentDeck.ResetDeck();
        foreach (int id in currentDeckID.cardIDList) {
            currentDeck.AddCard(prefabList[id]);            
        }
    }

    public void SaveJson(DeckID newDeck) {
        string json = JsonUtility.ToJson(newDeck);
        File.WriteAllText(deckPath, json);
    }

    public void Initialise() {
        deckPath = $"{Application.persistentDataPath}/deckID.json";
        if (File.Exists(deckPath)) {
            string json = File.ReadAllText(deckPath);
            currentDeckID = JsonUtility.FromJson<DeckID>(json);
            DeckIdToDeck();
        } else {
            currentDeck = new Deck();
            currentDeckID = new DeckID(); 
            currentDeckID.AddCardID(0);
            currentDeckID.AddCardID(0);
            currentDeckID.AddCardID(0);
            currentDeckID.AddCardID(0);
            currentDeckID.AddCardID(0);
            currentDeckID.AddCardID(1);
            currentDeckID.AddCardID(1);
            currentDeckID.AddCardID(1);
            currentDeckID.AddCardID(1);
            currentDeckID.AddCardID(1);
            SaveJson(currentDeckID);
            DeckIdToDeck();
        }
        unusedPile = currentDeck;
        Deck.Shuffle(unusedPile);
        DrawCard(5);
    }

    public void Draw(int numOfDraws) {
        for (int i = currentHand.cardList.Count; i < numOfDraws; i++) {
                currentHand.AddCard(unusedPile.cardList[0]);
                Instantiate(currentHand.cardList[i]).transform.SetParent(currentHandPanel.transform, false);
                unusedPile.RemoveCard(unusedPile.cardList[0]);
            }
    }

    public void DisableAllScripts(GameObject obj) {
        foreach (MonoBehaviour c in obj.GetComponents<MonoBehaviour>()) {
            if (!(c is CardDisplay)) {
                c.enabled = false;
            }
            
        }
    }

    public void GenerateNewCards() {
        string playerType = PlayerPrefs.GetString("character");
        List<GameObject> newList = null;
        Deck playerDeck;
        if (playerType == "Warrior") {
            newList = prefabList.GetRange(6, 10);
            playerDeck = new Deck(newList);
            Deck.Shuffle(playerDeck);
        } else if (playerType == "Archer") {
            newList = prefabList.GetRange(16, 10);
            playerDeck = new Deck(newList);
            Deck.Shuffle(playerDeck);
        } else {
            newList = prefabList.GetRange(26, 10);
            playerDeck = new Deck(newList);
            Deck.Shuffle(playerDeck);
        }
        for (int i = 0; i < 3; i ++) {
            GameObject temp = Instantiate(playerDeck.cardList[i]);
            temp.transform.SetParent(cardselectionPanel.transform, false);
            DisableAllScripts(temp);
            temp.AddComponent<CardSelection>();
        }
    }

    public void DrawCard(int numOfCards) {
        currentHand.ResetDeck();
        int numOfCardLeft = unusedPile.cardList.Count;
        if (numOfCardLeft < numOfCards) {
            this.Draw(numOfCardLeft);       
            usedPile.CopyTo(unusedPile);
            usedPile.ResetDeck();
            Deck.Shuffle(unusedPile);
            this.Draw(numOfCards - numOfCardLeft + currentHand.cardList.Count);
        } else {
            this.Draw(numOfCards);
        }
    }

    public void RerenderCards() {
        // destroy cards left on panel
        for (int i = 0; i < 5; i ++) {
            usedPile.AddCard(currentHand.cardList[i]);
        }
        foreach (Transform obj in currentHandPanel.transform) {
            GameObject.Destroy(obj.gameObject);
        }
        DrawCard(5);
    }
   
}
