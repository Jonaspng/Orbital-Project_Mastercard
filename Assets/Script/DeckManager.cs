using System.IO;
using UnityEngine;

public class DeckManager : MonoBehaviour {

    public string deckPath;
    
    public Deck currentDeck;

    public Deck usedPile;
    
    public Deck unusedPile;

    public Deck currentHand;

    void Start() {
        deckPath = $"{Application.persistentDataPath}/deck.json";
        if (File.Exists(deckPath)) {
            string json = File.ReadAllText(deckPath);
            currentDeck = JsonUtility.FromJson<Deck>(json);
        } else {
            currentDeck = new Deck();
            currentDeck.AddCard(new BasicAttack(6, 1, 1));
            currentDeck.AddCard(new BasicAttack(6, 1, 1));
            currentDeck.AddCard(new BasicAttack(6, 1, 1));
            currentDeck.AddCard(new BasicAttack(6, 1, 1));
            currentDeck.AddCard(new BasicAttack(6, 1, 1));
            currentDeck.AddCard(new Defend(6, 1, 1));
            currentDeck.AddCard(new Defend(6, 1, 1));
            currentDeck.AddCard(new Defend(6, 1, 1));
            currentDeck.AddCard(new Defend(6, 1, 1));
            currentDeck.AddCard(new Defend(6, 1, 1));
            SaveJson(currentDeck);
        }
    }

    public void SaveJson(Deck newDeck) {
        string json = JsonUtility.ToJson(newDeck);
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
