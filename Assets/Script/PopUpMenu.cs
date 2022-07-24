using UnityEngine;
using UnityEngine.SceneManagement;

public class PopUpMenu : MonoBehaviour  {
    [SerializeField] private GameObject cardSelectionMenu;

    [SerializeField] private GameObject confirmButton;

    [SerializeField] private GameObject NotificationMenu;

    [SerializeField] private int newCardId;
 
    void Start () {
        cardSelectionMenu.SetActive(false); 
    }

    public void SetNewCardID(int cardID) {
        this.newCardId = cardID;
    }
         
    public void PopUp()  { 
        cardSelectionMenu.SetActive (!cardSelectionMenu.activeSelf); 
        cardSelectionMenu.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void OnConfirmClick() {
        PlayerPrefs.SetInt("random event", 0); //reseted
        PlayerPrefs.SetInt("health", StageManager.GetInstance().GetPlayer().GetHealth());
        DeckID newDeck = StageManager.GetInstance().GetDeckManager().GetCurrentDeckID();
        newDeck.AddCardID(newCardId);
        StageManager.GetInstance().GetDeckManager().SaveJson(newDeck);
        PlayerPrefs.SetInt("stage", GameObject.Find("GameManager").GetComponent<GameManager>().GetStageNumber() + 1);
        foreach (Transform obj in GameObject.Find("NewCards").transform) {
            GameObject.Destroy(obj.gameObject);
        }
        this.PopUp();
        int stageNumber = GameObject.Find("GameManager").GetComponent<GameManager>().GetStageNumber() + 1;
        if (stageNumber == 3 || stageNumber == 6) {
            SceneManager.LoadScene("Event");
        } else {
            StageManager.GetInstance().InitialiseBattle();
        }
        
    }

    public void RenderConfirmButton() {
        confirmButton.SetActive(true);
    }

    public void NotificationConfirmClick() {
        NotificationMenu.SetActive(false);
    }
}
