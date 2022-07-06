using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MessageManager3 : MonoBehaviour {

    public TextMeshProUGUI message;
    [SerializeField] public TextWriter textWriter;
    public GameObject nextButton;

    private void Awake() {
        nextButton.SetActive(false);
    }
    public void OnNextButtonClick() {
        SceneManager.LoadScene("End Cutscene2");
    }

    private void Start() {
        string messageText = "Location: Unknown\nT-Minus: Unknown\n\nThank you so much for freeing the minds of catkind.\n\nWe are forever indebted to your actions.\n\nI will now send you back to your world.\n\nAlways keep this in mind, young one: No matter what happens, SU will always be your friend.";
        textWriter.AddWriter(message, messageText, 0.05f);
    }

    
    public void OnMouseDown() {
        GameObject.Find("Text Writer").GetComponent<TextWriter>().OnSkipButtonClick();
    }
  
}
