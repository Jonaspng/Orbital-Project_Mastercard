using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DeckID {

    public List<int> cardIDList;

    public DeckID() {   
        this.cardIDList = new List<int>(); 
    }

    public void AddCardID(int cardID) {
        this.cardIDList.Add(cardID);
    }

    public void RemoveCard(int cardID) {
        this.cardIDList.Remove(cardID);
    }

    public void CopyTo(DeckID deck) {
        List<int> newList = new List<int>(this.cardIDList);
        deck.cardIDList = newList;
    }


    
   
}
