using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Deck {

    [SerializeField] private List<GameObject> cardList;

    public List<GameObject> GetCardList() {
        return this.cardList;
    }

    public Deck() {   
        this.cardList = new List<GameObject>(); 
    }

    public Deck(List<GameObject> cardList) {   
        this.cardList = cardList; 
    }

    public void AddCard(GameObject card) {
        this.cardList.Add(card);
    }

    public void RemoveCard(GameObject card) {
        this.cardList.Remove(card);
    }

    public void CopyTo(Deck deck) {
        List<GameObject> newList = new List<GameObject>(this.cardList);
        deck.cardList = newList;
    }

    public void ResetDeck() {
        this.cardList.RemoveAll(x => true);
    }

    public static void Shuffle(Deck deck) {
         System.Random random = new System.Random();
         for( int i = 0; i < deck.cardList.Count; i++ ) {
             int j = random.Next( i, deck.cardList.Count );
             GameObject temporary = deck.cardList[i];
             deck.cardList[i] = deck.cardList[j];
             deck.cardList[j] = temporary;
         }
     }

    
   
}
