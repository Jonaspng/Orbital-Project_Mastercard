using System.IO;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DeckManager : MonoBehaviour {

    public string deckPath;

    public DeckID currentDeckID;
    
    public Deck currentDeck;

    public Deck usedPile;
    
    public Deck unusedPile;

    public Deck currentHand;

    public List<GameObject> prefabList;


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

    public void SaveJson(DeckID newDeckId) {
        string json = JsonUtility.ToJson(newDeckId);
        File.WriteAllText(deckPath, json);
    }

    public void Initialise() {
        unusedPile = currentDeck;
        Deck.Shuffle(unusedPile);
    }

    public void Reshuffle(int numOfShuffles) {
        for (int i = 0; i < numOfShuffles; i++) {
                currentHand.AddCard(unusedPile.cardList[i]);
                unusedPile.RemoveCard(unusedPile.cardList[i]);
                usedPile.AddCard(unusedPile.cardList[i]);
            }
    }

    public void DrawCard(int numOfCards) {
        int numOfCardLeft = unusedPile.cardList.Count;
        if (numOfCardLeft < numOfCards) {
            // for (int i = 0; i < numOfCardLeft; i++) {
            //     currentHand.AddCard(unusedPile.cardList[i]);
            //     unusedPile.RemoveCard(unusedPile.cardList[i]);
            //     usedPile.AddCard(unusedPile.cardList[i]);
            // }
            this.Reshuffle(numOfCardLeft);       
            usedPile.CopyTo(unusedPile);
            usedPile.ResetDeck();
            Deck.Shuffle(unusedPile);
            // for (int i = 0; i < numOfCards - numOfCardLeft; i++) {
            //     currentHand.AddCard(unusedPile.cardList[i]);
            //     unusedPile.RemoveCard(unusedPile.cardList[i]);
            //     usedPile.AddCard(unusedPile.cardList[i]);
            // }
            this.Reshuffle(numOfCards - numOfCardLeft);
        } else {
            this.Reshuffle(numOfCards);
        }
    }
   
}
