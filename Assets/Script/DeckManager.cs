using System.IO;
using UnityEngine;
using System.Collections.Generic;

[DefaultExecutionOrder(-1)]
[System.Serializable]
public class DeckManager : MonoBehaviour {

    [SerializeField] private string deckPath;

    [SerializeField] private DeckID currentDeckID;
    
    [SerializeField] private Deck currentDeck;

    [SerializeField] private Deck usedPile;
    
    [SerializeField] private Deck unusedPile;

    [SerializeField] private Deck currentHand;

    [SerializeField] private int lockedCard;
    
    [SerializeField] private List<GameObject> prefabList;

    [SerializeField] private GameObject currentHandPanel;

    [SerializeField] private GameObject cardselectionPanel;

    [SerializeField] private Material cardOutline;

    public Deck GetCurrentDeck() {
        return this.currentDeck;
    }

    public int GetLockedCard() {
        return this.lockedCard;
    }

    public DeckID GetCurrentDeckID() {
        return this.currentDeckID;
    }

    public void LockCard() {
        lockedCard = Random.Range(0, currentDeckID.GetCardIDList().Count);
        unusedPile.RemoveCard(currentDeck.GetCardList()[lockedCard]);
    }

    public void DeckIdToDeck() {
        currentDeck.ResetDeck();
        unusedPile.ResetDeck();
        usedPile.ResetDeck();
        foreach (int id in currentDeckID.GetCardIDList()) {
            currentDeck.AddCard(prefabList[id]);
            unusedPile.AddCard(prefabList[id]);       
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
            string playerType = PlayerPrefs.GetString("character");
            currentDeck = new Deck();
            currentDeckID = new DeckID(); 
            if (playerType == "Warrior") {
                for (int i = 0; i < 5; i++) {
                    currentDeckID.AddCardID(0);
                }
                for (int j = 0; j < 5; j++) {
                    currentDeckID.AddCardID(1);
                }
            } else if (playerType == "Archer") {
                for (int i = 0; i < 5; i++) {
                    currentDeckID.AddCardID(2);
                }
                for (int j = 0; j < 5; j++) {
                    currentDeckID.AddCardID(3);
                }
            } else {
                for (int i = 0; i < 5; i++) {
                    currentDeckID.AddCardID(4);
                }
                for (int j = 0; j < 5; j++) {
                    currentDeckID.AddCardID(5);
                }
            }
            SaveJson(currentDeckID);
            DeckIdToDeck();
        }
        Deck.Shuffle(unusedPile);
    }

    public void Draw(int numOfDraws) {
        for (int i = currentHand.GetCardList().Count; i < numOfDraws; i++) {
                currentHand.AddCard(unusedPile.GetCardList()[0]);
                Instantiate(currentHand.GetCardList()[i]).transform.SetParent(currentHandPanel.transform, false);
                unusedPile.RemoveCard(unusedPile.GetCardList()[0]);
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
            GameObject temp = Instantiate(playerDeck.GetCardList()[i]);
            temp.transform.SetParent(cardselectionPanel.transform, false);
            temp.GetComponent<Cards>().DisableAllScripts();
            temp.AddComponent<CardSelection>();
            temp.GetComponent<CardSelection>().SetOutline(cardOutline);
        }
    }

    public void DrawCard(int numOfCards) {
        currentHand.ResetDeck();
        int numOfCardLeft = unusedPile.GetCardList().Count;
        if (numOfCardLeft < numOfCards) {
            this.Draw(numOfCardLeft);       
            usedPile.CopyTo(unusedPile);
            usedPile.ResetDeck();
            Deck.Shuffle(unusedPile);
            this.Draw(numOfCards - numOfCardLeft + currentHand.GetCardList().Count);
        } else {
            this.Draw(numOfCards);
        }
        GameObject.Find("Current Hand").GetComponent<FanShapeArranger>().ArrangeCards();
    }

    public void ClearCards() {
        // destroy cards left on panel
        for (int i = 0; i < 5; i ++) {
            usedPile.AddCard(currentHand.GetCardList()[i]);
        }
        foreach (Transform obj in currentHandPanel.transform) {
            GameObject.Destroy(obj.gameObject);
        }
    }
   
}
