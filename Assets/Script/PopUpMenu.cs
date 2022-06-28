using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpMenu : MonoBehaviour  {
    public GameObject cardSelectionMenu;

    public GameObject confirmButton;

    public int newCardId;
 
    void Start () {
        cardSelectionMenu.SetActive(false); 
    }
         
    public void PopUp()  { 
        cardSelectionMenu.SetActive (!cardSelectionMenu.activeSelf); 
        cardSelectionMenu.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void OnConfirmClick() {
        PlayerPrefs.SetInt("health", StageManager.instance.player.health);
        DeckID newDeck = StageManager.instance.deckManager.currentDeckID;
        newDeck.AddCardID(newCardId);
        StageManager.instance.deckManager.SaveJson(newDeck);
        PlayerPrefs.SetInt("stage", GameObject.Find("GameManager").GetComponent<GameManager>().stageNumber + 1);
        foreach (Transform obj in GameObject.Find("NewCards").transform) {
            GameObject.Destroy(obj.gameObject);
        }
        this.PopUp();
        int stageNumber = GameObject.Find("GameManager").GetComponent<GameManager>().stageNumber + 1;
        if (stageNumber == 3 || stageNumber == 6) {
            SceneManager.LoadScene("Event");
        } else {
            StageManager.instance.InitialiseBattle();
        }
        
    }

    public void RenderConfirmButton() {
        confirmButton.SetActive(true);
    }
}
