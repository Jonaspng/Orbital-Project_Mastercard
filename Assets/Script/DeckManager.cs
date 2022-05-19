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

    public GameObject panel;



    void Start() {
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
    }

     public void DeckIdToDeck() {
        foreach (int id in currentDeckID.cardIDList) {
            currentDeck.AddCard(prefabList[id]);            
        }
    }

    public void SaveJson(DeckID newDeck) {
        string json = JsonUtility.ToJson(newDeck);
        File.WriteAllText(deckPath, json);
    }

    public void Initialise() {
        unusedPile = currentDeck;
        Deck.Shuffle(unusedPile);
        DrawCard(5);
    }

    public void Draw(int numOfDraws) {
        for (int i = 0; i < numOfDraws; i++) {
                currentHand.AddCard(unusedPile.cardList[0]);
                Instantiate(currentHand.cardList[i]).transform.SetParent(panel.transform, false);
                usedPile.AddCard(unusedPile.cardList[0]);
                unusedPile.RemoveCard(unusedPile.cardList[0]);
            }
    }

    public void DrawCard(int numOfCards) {
        currentHand.ResetDeck();
        int numOfCardLeft = unusedPile.cardList.Count;
        if (numOfCardLeft < numOfCards) {
            print("test");
            this.Draw(numOfCardLeft);       
            usedPile.CopyTo(unusedPile);
            usedPile.ResetDeck();
            Deck.Shuffle(unusedPile);
            this.Draw(numOfCards - numOfCardLeft);
        } else {
            this.Draw(numOfCards);
        }
    }

    public void RerenderCards() {
        // destroy cards left on panel
        foreach (Transform obj in panel.transform) {
            GameObject.Destroy(obj.gameObject);
        }

        DrawCard(5);
    }
   
}
