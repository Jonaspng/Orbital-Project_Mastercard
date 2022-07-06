using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MessageManager2 : MonoBehaviour {

    public TextMeshProUGUI message;
    [SerializeField] public TextWriter textWriter;
    public GameObject nextButton;

    private void Awake() {
        nextButton.SetActive(false);
    }
    public void OnNextButtonClick() {
        SceneManager.LoadScene("Character Selection");
    }

    private void Start() {
        string messageText = "Location: Unknown\nT-Minus: Unknown\n\nHello Stranger. You don't look like you are from around here.\nI am Sage-Ultra. You may call me SU.\n\nLet me explain what is happening here. You see an evil cat called CS3230 has infected the minds of cats residing in this dungeon.\n\nI believe your arrival is not a coincidence but the will of CS GODs.\n\nI beseech you, please save our world.\n\nYou shall not be alone in this fight. I shall grant you the power to combat.\n";
        textWriter.AddWriter(message, messageText, 0.05f);
    }

    public void OnMouseDown() {
        GameObject.Find("Text Writer").GetComponent<TextWriter>().OnSkipButtonClick();
    }
  
}
