using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class MessageManager4 : MonoBehaviour {

    public TextMeshProUGUI message;
    [SerializeField] public TextWriter textWriter;
    public GameObject nextButton;

    string deckPath;

    private void Awake() {
        nextButton.SetActive(false);
        deckPath = $"{Application.persistentDataPath}/deckID.json";
        File.Delete(deckPath);
    }

    public void OnNextButtonClick() {
        SceneManager.LoadScene("Start Menu");
    }


    private void Start() {
        string messageText = "Location: Comfortable bed at home\nT-Minus: 0 days to result release (It's results day!)\n\nYou wake up in your own bed, covered in sweat. Wow, what a weird dream that was, you think to yourself. Suddenly it dawned on you that today was in fact results day.However, instead of feeling anxious or stressed about doing badly, you feel strangely calm and peaceful. It's as if someone rewired the emotions in your mind. \"results don't matter, SU is my friend\", you find yourself repeating those words. And with that mantra in mind, you confidently open the university portal and click on the view results button.";
        textWriter.AddWriter(message, messageText, 0.1f);
    }
  
}
