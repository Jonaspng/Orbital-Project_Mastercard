using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameSelectionManager : MonoBehaviour {

    string deckPath;

    // Start is called before the first frame update
    void Start() {
        deckPath = $"{Application.persistentDataPath}/deckID.json";
        if (!File.Exists(deckPath)) {
            GameObject.Find("ResumeButton").SetActive(false);
        }
        this.gameObject.SetActive(false);
    }

    public void OnResumeClick() {
        SceneManager.LoadScene("Stage 1");
        PlayerPrefs.SetInt("random event", 0);
    }

    public void OnStartNewGameClick() {
        SceneManager.LoadScene("Character Selection");
        File.Delete(deckPath);
        PlayerPrefs.SetInt("stage", 1);
        PlayerPrefs.SetInt("random event", 0);
    }

    
}