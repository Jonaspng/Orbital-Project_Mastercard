using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour {

    public GameObject gameSelectionMenu;

    public GameObject libraryMenu;

    public GameObject settingsMenu;


    private void Awake() {
        if (!PlayerPrefs.HasKey("volumeValue")) {
            PlayerPrefs.SetFloat("volumeValue", 1.0f);
        }
        AudioListener.volume = PlayerPrefs.GetFloat("volumeValue");
    }

    public void OnStartClick() {
        gameSelectionMenu.SetActive(true);
    }

    public void OnSettingsClick() {
        settingsMenu.SetActive(true);
    }

    public void OnCreditsClick() {
        SceneManager.LoadScene("Credits");
    }

    public void OnLibraryClick() {
        libraryMenu.SetActive(true);
    }
}
