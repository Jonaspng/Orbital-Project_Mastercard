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
            currentDeck.AddCard(Instantiate(prefabList[id]));            
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

    // public void Reshuffle(int numOfShuffles) {
    //     for (int i = 0; i < numOfShuffles; i++) {
    //             currentHand.AddCard(unusedPile.cardList[i]);
    //             unusedPile.RemoveCard(unusedPile.cardList[i]);
    //             usedPile.AddCard(unusedPile.cardList[i]);
    //         }
    // }

    
    public void DrawCard(int numOfCards) {
        int numOfCardLeft = unusedPile.cardList.Count;
        if (numOfCardLeft < numOfCards) {
           print("test");
            for (int i = 0; i < numOfCards; i++) {
                currentHand.AddCard(unusedPile.cardList[i]);
                currentHand.cardList[i].transform.SetParent(panel.transform);
                unusedPile.RemoveCard(unusedPile.cardList[i]);
                usedPile.AddCard(unusedPile.cardList[i]);
            }
            //this.Reshuffle(numOfCardLeft);       
            // usedPile.CopyTo(unusedPile);
            // usedPile.ResetDeck();
            // Deck.Shuffle(unusedPile);
            // this.Reshuffle(numOfCards - numOfCardLeft);
        } else {
             print("test");
            for (int i = 0; i < numOfCards; i++) {
                currentHand.AddCard(unusedPile.cardList[i]);
                currentHand.cardList[i].transform.SetParent(panel.transform);
                unusedPile.RemoveCard(unusedPile.cardList[i]);
                usedPile.AddCard(unusedPile.cardList[i]);
            }
            //this.Reshuffle(numOfCards);
        }
    }
   
}
