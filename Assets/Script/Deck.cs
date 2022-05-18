using System.Collections.Generic;

public class Deck {

    public List<Cards> cardList;


    public Deck() {
        this.cardList = new List<Cards>();        
    }

    public void AddCard(Cards card) {
        this.cardList.Add(card);
    }

    public void RemoveCard(Cards card) {
        this.cardList.Remove(card);
    }

    public void CopyTo(Deck deck) {
        List<Cards> newList = new List<Cards>(this.cardList);
        deck.cardList = newList;
    }

    public void ResetDeck() {
        this.cardList.RemoveAll(x => true);
    }

    public static void Shuffle(Deck deck) {
         System.Random random = new System.Random();
         for( int i = 0; i < deck.cardList.Count; i++ ) {
             int j = random.Next( i, deck.cardList.Count );
             Cards temporary = deck.cardList[i];
             deck.cardList[i] = deck.cardList[j];
             deck.cardList[j] = temporary;
         }
     }

    
   
}
