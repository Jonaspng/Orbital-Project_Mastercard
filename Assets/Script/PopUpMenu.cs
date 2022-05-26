using UnityEngine;
public class PopUpMenu : MonoBehaviour  {
    public GameObject cardSelectionMenu;

    public GameObject confirmButton;

    public int newCardId;
 
    void Start ()  {
        cardSelectionMenu.SetActive(false); 
    }
         
    public void PopUp()  { 
        cardSelectionMenu.SetActive (!cardSelectionMenu.activeSelf); 
        cardSelectionMenu.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void OnConfirmClick() {
        DeckID newDeck = StageManager.instance.deckManager.currentDeckID;
        newDeck.AddCardID(newCardId);
        StageManager.instance.deckManager.SaveJson(newDeck);
        GameObject.Find("GameManager").GetComponent<GameManager>().stageNumber += 1;
        foreach (Transform obj in GameObject.Find("NewCards").transform) {
            GameObject.Destroy(obj.gameObject);
        }
        GameObject.Find("StageManager").GetComponent<PopUpMenu>().PopUp();
        
        StageManager.instance.InitialiseBattle();
    }

    public void RenderConfirmButton() {
        confirmButton.SetActive(true);
    }
}
