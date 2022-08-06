using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MessageManager1 : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private TextWriter textWriter;
    [SerializeField] private GameObject nextButton;

    private void Awake() {
        nextButton.SetActive(false);
    }

    public void OnNextButtonClick() {
        SceneManager.LoadScene("Intro Cutscene2");
    }

    private void Start() {
        string messageText = "Location: NUS School of Computing (School World Ranking: 4th)\nT-Minus: 1 day before result release\n\nYou are a student of the prestigious NUS School of Computing. However, you constantly worry about your results due to fierce competiton and steep bellcurve. Tonight, while laying on your bed, you toss and turn and find it hard to sleep. When you finally do, you begin to enter a dream unlike any you've had before...";
        textWriter.AddWriter(message, messageText, 0.05f);
    }

    public void OnMouseDown() {
        GameObject.Find("Text Writer").GetComponent<TextWriter>().OnSkipButtonClick();
    }
  
}
